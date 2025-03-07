using HarmonyLib;
using System;
using System.Collections.Generic;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Hidden Folder
    [HarmonyPatch(typeof(ResourceManager), "LevelPrefabFileInfosRoot_", MethodType.Getter)]
    internal static class ResourceManager__LevelPrefabFileInfosRoot_
    {
        [HarmonyPostfix]
        internal static void Postfix(ref LevelPrefabFileInfo __result)
        {
            bool hasHiddenFolder = false;
            foreach (LevelPrefabFileInfo chile in __result.childInfos_)
            {
                if ((chile.name_.IsNullOrWhiteSpace() ? "null" : chile.name_).Equals("Hidden"))
                {
                    hasHiddenFolder = true;
                    if (!Mod.HiddenFolderEnabled.Value)
                    {
                        Predicate<LevelPrefabFileInfo> predicate = new Predicate<LevelPrefabFileInfo>(Mod.CheckWhichIsHiddenFolder);
                        __result.RemoveAllChildInfos(predicate);
                        return;
                    }
                }
            }
            if (hasHiddenFolder && Mod.HiddenFolderEnabled.Value) return;
            if (!Mod.HiddenFolderEnabled.Value) return;
            ResourceManager resourceManager = G.Sys.ResourceManager_;
            Dictionary<string, List<string>> resourceMap_ = resourceManager.resourceMap_;
            LevelPrefabFileInfo folder = new LevelPrefabFileInfo("Hidden", __result);
            LevelPrefabFileInfo audiofolder = new LevelPrefabFileInfo("Audio", folder);
            LevelPrefabFileInfo carfolder = new LevelPrefabFileInfo("Car", folder);
            LevelPrefabFileInfo archfolder = new LevelPrefabFileInfo("Archive", carfolder);
            LevelPrefabFileInfo catafolder = new LevelPrefabFileInfo("Catalyst", carfolder);
            LevelPrefabFileInfo hatsfolder = new LevelPrefabFileInfo("Hats", carfolder);
            LevelPrefabFileInfo refractorfolder = new LevelPrefabFileInfo("Refractor", carfolder);
            carfolder.AddChildInfo(archfolder);
            carfolder.AddChildInfo(catafolder);
            carfolder.AddChildInfo(hatsfolder);
            carfolder.AddChildInfo(refractorfolder);
            LevelPrefabFileInfo detfolder = new LevelPrefabFileInfo("Detonator", folder);
            LevelPrefabFileInfo dirtyfolder = new LevelPrefabFileInfo("DirtyLens", folder);
            LevelPrefabFileInfo analfolder = new LevelPrefabFileInfo("GameAnalytics", folder);
            LevelPrefabFileInfo gamemodefolder = new LevelPrefabFileInfo("GameModes", folder);
            LevelPrefabFileInfo hoverscreenfolder = new LevelPrefabFileInfo("HoverScreen", folder);
            LevelPrefabFileInfo legacyguifolder = new LevelPrefabFileInfo("LegacyGUI", folder);
            LevelPrefabFileInfo lvleditprivatefolder = new LevelPrefabFileInfo("LevelEditorPrivate", folder);
            LevelPrefabFileInfo mngerfolder = new LevelPrefabFileInfo("Managers", folder);
            LevelPrefabFileInfo replayfolder = new LevelPrefabFileInfo("Replays", folder);
            LevelPrefabFileInfo settingsfolder = new LevelPrefabFileInfo("Settings", folder);
            LevelPrefabFileInfo trackmogfolder = new LevelPrefabFileInfo("Trackmogrify", folder);
            folder.AddChildInfo(audiofolder);
            folder.AddChildInfo(carfolder);
            folder.AddChildInfo(detfolder);
            folder.AddChildInfo(dirtyfolder);
            folder.AddChildInfo(analfolder);
            folder.AddChildInfo(gamemodefolder);
            folder.AddChildInfo(hoverscreenfolder);
            folder.AddChildInfo(legacyguifolder);
            folder.AddChildInfo(lvleditprivatefolder);
            folder.AddChildInfo(mngerfolder);
            folder.AddChildInfo(replayfolder);
            folder.AddChildInfo(settingsfolder);
            folder.AddChildInfo(trackmogfolder);



            foreach (KeyValuePair<string, List<string>> kvp in resourceMap_)
            {
                foreach (string path in kvp.Value)
                {
                    if (path.Contains("Prefabs") && !path.Contains("LevelBackups") && !path.Contains("/Custom/") && !path.Contains("/LevelEditor/") && !path.Contains("LevelEditorMenus") && !path.Contains("Menus") && !path.Contains("Managers") && !path.Contains("wheel") && !path.Contains("ExampleListView"))
                    {
                        LevelPrefabFileInfo parentdirec = null;
                        if (path.Contains("/Audio/")) parentdirec = audiofolder;
                        else if (path.Contains("/Car/"))
                        {
                            if (path.Contains("/Archive/")) parentdirec = archfolder;
                            else if (path.Contains("/Catalyst/")) parentdirec = catafolder;
                            else if (path.Contains("/Hats/")) parentdirec = hatsfolder;
                            else if (path.Contains("/Refractor/")) parentdirec = refractorfolder;
                            else parentdirec = carfolder;
                        }
                        else if (path.Contains("/Detonator/")) parentdirec = detfolder;
                        else if (path.Contains("/DirtyLens/")) parentdirec = dirtyfolder;
                        else if (path.Contains("/GameAnalytics/")) parentdirec = analfolder;
                        else if (path.Contains("/GameModes/")) parentdirec = gamemodefolder;
                        else if (path.Contains("/HoverScreen/")) parentdirec = hoverscreenfolder;
                        else if (path.Contains("/LegacyGUI/")) parentdirec = legacyguifolder;
                        else if (path.Contains("/LevelEditorPrivate/")) parentdirec = lvleditprivatefolder;
                        else if (path.Contains("/Managers/")) parentdirec = mngerfolder;
                        else if (path.Contains("/Replays/")) parentdirec = replayfolder;
                        else if (path.Contains("/Settings/")) parentdirec = settingsfolder;
                        else if (path.Contains("/Trackmogrify/")) parentdirec = trackmogfolder;
                        UnityEngine.Object resourceType = UnityEngine.Resources.Load(path, typeof(UnityEngine.Object)) as UnityEngine.Object;
                        if ((UnityEngine.Object)resourceType != (UnityEngine.Object)null)
                        {
                            UnityEngine.GameObject hiddenObject = (UnityEngine.Object)resourceManager != (UnityEngine.Object)null ? resourceManager.GetResource<UnityEngine.GameObject>(kvp.Key, true) : UnityEngine.Resources.Load<UnityEngine.GameObject>(kvp.Key);
                            LevelPrefabFileInfo hiddenObjInfo;
                            if (parentdirec == null)
                            {
                                hiddenObjInfo = new LevelPrefabFileInfo(kvp.Key, hiddenObject, folder);
                                folder.AddChildInfo(hiddenObjInfo);
                            }
                            else
                            {
                                //Mod.Logger.Info("This should Work!!!!!!");
                                hiddenObjInfo = new LevelPrefabFileInfo(kvp.Key, hiddenObject, parentdirec);
                                parentdirec.AddChildInfo(hiddenObjInfo);
                            }

                        }
                    }
                }

            }

            __result.AddChildInfo(folder);

        }
    }
}
