using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Remove Mode Requirements to Save
    [HarmonyPatch(typeof(GameManager), "GetModeCheckRequirements")]
    internal static class GameManager__GetModeCheckRequirements
    {
        [HarmonyPrefix]
        internal static bool Prefix(ref CheckModeRequirements __result)
        {

            if (Mod.RemoveModeRequirements.Value)
            {
                __result = null;
                return false;
            }
            return true;
        }
    }
}
