using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Linq;

namespace EditorExpanded
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Mod : BaseUnityPlugin
    {
        //Mod Details
        private const string modGUID = "Distance.EditorExpanded";
        private const string modName = "Editor Expanded";
        private const string modVersion = "1.0.0";

        //Config Entry Strings
        public static string DevFolderKey = "Enable Dev Folder";
        public static string AdvancedMusicSelectionKey = "Advanced Music Selection";
        public static string DisplayWorkshopKey = "Display Workshop Levels";
        public static string EditorIconKey = "Editor Icon Size";
        public static string EnableGroupKey = "Enable Group Centerpoint Mover";
        public static string UnlimitedMedalKey = "Unlimited Medal Times";
        public static string RemoveLimitsKey = "Remove Editor Number Limits";
        public static string RemoveRequirementsKey = "Remove Mode Requirements";
        public static string MultipleCarKey = "Multiple Car Spawners";
        public static string HiddenFolderKey = "Enable Hidden Folder";
        public static string UnlimitedComponentKey = "Unlimited Add Component";
        public static string AllChangeTrackKey = "Change Track Type Includes All Splines";
        public static string SubTextureKey = "Enable Sub Materials";
        public static string EditorPrecisionKey = "Decimal Precision";
        public static string CursorBackgroundKey = "Cursor Ignores Background Layer";
        public static string EnableHiddenComponentKey = "Enable Hidden Components";
        public static string RemoveCreatorKey = "Remove Level Creator Field";
        public static string EnableAllModesKey = "Enable All Modes";

        //Config Entries
        public static ConfigEntry<bool> DevFolderEnabled { get; set; }
        public static ConfigEntry<bool> AdvancedMusicSelection { get; set; }
        public static ConfigEntry<bool> DisplayWorkshopLevels { get; set; }
        public static ConfigEntry<float> EditorIconSize { get; set; }
        public static ConfigEntry<bool> EnableGroupCenterMover { get; set; }
        public static ConfigEntry<bool> UnlimitedMedalTimes { get; set; }
        public static ConfigEntry<bool> RemoveNumberLimits { get; set; }
        public static ConfigEntry<bool> RemoveModeRequirements { get; set; }
        public static ConfigEntry<bool> MultipleCarSpawners { get; set; }
        public static ConfigEntry<bool> HiddenFolderEnabled { get; set; }
        public static ConfigEntry<bool> UnlimitedAddComponent { get; set; }
        public static ConfigEntry<bool> IncludeAllSplinesInChangeTrackType { get; set; }
        public static ConfigEntry<bool> EnableSubTextures { get; set; }
        public static ConfigEntry<int> EditorDecimalPrecision { get; set; }
        public static ConfigEntry<bool> CursorIgnoresBackground { get; set; }
        public static ConfigEntry<bool> EnableHiddenComponent { get; set; }
        public static ConfigEntry<bool> RemoveCreatorField { get; set; }
        public static ConfigEntry<bool> EnableAllModes { get; set; }

        //Public Variables
        public static bool DevMode => IsCommandLineSwitchPresent("-dev");
        public static bool DevBuildForCreatorName { get; set; }
        public static bool IsCommandLineSwitchPresent(string item)
        {
            return Environment.GetCommandLineArgs().Select(arg => arg.ToLower()).Contains(item);
        }
        public TrackNodeColors TrackNodeColors { get; set; }

        //Other
        private static readonly Harmony harmony = new Harmony(modGUID);
        public static ManualLogSource Log = new ManualLogSource(modName);
        public static Mod Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Log = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            Logger.LogInfo("Thanks for using Editor Expanded!");

            TrackNodeColors = TrackNodeColors.FromSettings("SplineColors.json");
            TrackNodeColors.OnFileReloaded += ReloadTrackNodeColors;


            //Config Setup
            DevFolderEnabled = Config.Bind<bool>("General",
                DevFolderKey,
                true,
                new ConfigDescription("Enables the Dev folder in the level editor Library tab.\nSome of the objects in this folder might not work properly, be careful when using them!"));

            RemoveNumberLimits = Config.Bind<bool>("General",
                RemoveLimitsKey,
                true,
                new ConfigDescription("Removes number limits from the editor. This will allow negative values for many settings. Be sure to get test the level in sprint mode. Some values do not get interpreted properly."));

            EnableGroupCenterMover = Config.Bind<bool>("General",
                EnableGroupKey,
                true,
                new ConfigDescription("Enables the group centerpoint mover option."));

            AdvancedMusicSelection = Config.Bind<bool>("General",
                AdvancedMusicSelectionKey,
                true,
                new ConfigDescription("Display hidden dev music cues in the level editor music selector window."));

            DisplayWorkshopLevels = Config.Bind<bool>("General",
                DisplayWorkshopKey,
                false,
                new ConfigDescription("Allows to open the workshop levels from the level editor (read only)."));

            IncludeAllSplinesInChangeTrackType = Config.Bind<bool>("General",
                AllChangeTrackKey,
                true,
                new ConfigDescription("All splines are now included in the Change Track Type tool"));

            EnableSubTextures = Config.Bind<bool>("General",
                SubTextureKey,
                false,
                new ConfigDescription("Now displays sub textures in the properties menu for objects. This is good for coloring individual parts of an object you couldn't before."));

            CursorIgnoresBackground = Config.Bind<bool>("General",
                CursorBackgroundKey,
                false,
                new ConfigDescription("The Level Editor Cursor will no longer collide with background objects."));

            EditorDecimalPrecision = Config.Bind<int>("General",
                EditorPrecisionKey,
                3,
                new ConfigDescription("Sets the precision of decimals displayed in the editor. Range 0-10.",
                    new AcceptableValueRange<int>(0, 10)));

            EnableHiddenComponent = Config.Bind<bool>("General",
                EnableHiddenComponentKey,
                false,
                new ConfigDescription("Enables the visibility of components that normally are ignored. The box trigger on a killgridbox for example."));

            UnlimitedMedalTimes = Config.Bind<bool>("General",
                UnlimitedMedalKey,
                false,
                new ConfigDescription("Uncaps the medal time limit and allows any time to be set. So for example, a diamond medal can have a lower time than a bronze medal."));

            UnlimitedAddComponent = Config.Bind<bool>("General",
                UnlimitedComponentKey,
                false,
                new ConfigDescription("Allows components to be added to any object. \nWARNING: This can create very strange behaviour with certain objects, test the level in arcade mode to make sure it works!"));

            RemoveCreatorField = Config.Bind<bool>("General",
                RemoveCreatorKey,
                false,
                new ConfigDescription("Disables the visibility of the Level Creator Name field in the level settings. This field is only used by Official tier community levels."));

            RemoveModeRequirements = Config.Bind<bool>("General",
                RemoveRequirementsKey,
                false,
                new ConfigDescription("Removes the requirements needed to save a level. \nRemember to playtest your level in arcade mode before uploading to the workshop."));

            MultipleCarSpawners = Config.Bind<bool>("General",
                MultipleCarKey,
                false,
                new ConfigDescription("Allows the use of multiple Level Editor Car Spawners as well as multiple Tag Bubbles. The extra ones are unused by the game if placed."));

            EnableAllModes = Config.Bind<bool>("General",
                EnableAllModesKey,
                false,
                new ConfigDescription("Allows all modes to be visible in the Level Settings menu. Modes outside of Sprint, Challenge, Stunt, Reverse Tag, and Trackmogrify are unplayable. \nIt might be a good idea to keep this disabled."));

            HiddenFolderEnabled = Config.Bind<bool>("General",
                HiddenFolderKey,
                false,
                new ConfigDescription("Adds a folder to titled \"Hidden\" to the editor that contains objects not listed anywhere; including the dev folder. \nWARNING: These objects are not meant to be editor objects, they may corrupt level files or crash the game."));

            EditorIconSize = Config.Bind<float>("General",
                EditorIconKey,
                67f,
                new ConfigDescription("Adjusts the size of editor icons in the Library tab. \nThis value should be adjusted from the zoom slider in the library tab, not in this menu.",
                    new AcceptableValueRange<float>(32f, 256f)));


            //Apply Patches
            Logger.LogInfo("Loading...");
            harmony.PatchAll();
            Logger.LogInfo("Loaded!");
        }

        private void OnConfigChanged(object sender, EventArgs e)
        {
            SettingChangedEventArgs settingChangedEventArgs = e as SettingChangedEventArgs;

            if (settingChangedEventArgs == null) return;
        }

        private void ReloadTrackNodeColors(object sender, EventArgs e)
        {
            foreach (var node in FindObjectsOfType<TrackManipulatorNode>())
            {
                node.SetColorAndMesh();
            }
        }

        public static bool CheckWhichIsHiddenFolder(LevelPrefabFileInfo info)
        {
            if ((info.name_.IsNullOrWhiteSpace() ? "null" : info.name_).Equals("Hidden"))
            {
                return true;
            }
            return false;
        }
    }
}
