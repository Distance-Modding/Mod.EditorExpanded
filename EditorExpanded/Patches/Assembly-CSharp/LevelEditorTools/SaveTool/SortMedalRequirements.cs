using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Unlimited Medal Time
    [HarmonyPatch(typeof(SaveTool), "SortMedalRequirements")]
    internal static class SaveTool__SortMedalRequirements
    {
        [HarmonyPrefix]
        internal static bool Prefix()
        {
            if (Mod.UnlimitedMedalTimes.Value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
