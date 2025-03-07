using HarmonyLib;
using System.Collections.Generic;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Enable All Modes
    [HarmonyPatch(typeof(LevelSettings), "GetDefaultModesMap")]
    internal static class LevelSettings__GetDefaultModesMap
    {
        [HarmonyPostfix]
        internal static void Postfix(ref Dictionary<int, bool> __result)
        {
            if (!Mod.EnableAllModes.Value) return;
            __result.Add((int)GameModeID.None, false);
            __result.Add((int)GameModeID.FreeRoam, false);
            __result.Add((int)GameModeID.CoopSprint, false);
            __result.Add((int)GameModeID.Count, false);
        }
    }
}
