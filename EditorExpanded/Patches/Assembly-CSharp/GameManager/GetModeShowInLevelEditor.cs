using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Enable All Modes
    [HarmonyPatch(typeof(GameManager), "GetModeShowInLevelEditor")]
    internal static class GameManager__GetModeShowInLevelEditor
    {
        [HarmonyPostfix]
        internal static void Postfix(ref bool __result, GameModeID ID)
        {
            if (!Mod.EnableAllModes.Value)
            {

            }
            else
            {
                __result = true;
            }
        }
    }
}
