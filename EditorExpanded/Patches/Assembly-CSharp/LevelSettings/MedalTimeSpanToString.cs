using HarmonyLib;
using System;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Unlimited Medal Time
    [HarmonyPatch(typeof(LevelSettings), "MedalTimeSpanToString")]
    internal static class LevelSettings__MedalTimeSpanToString
    {
        [HarmonyPrefix]
        internal static bool Prefix(ref string __result, TimeSpan timeSpan)
        {
            if (Mod.UnlimitedMedalTimes.Value)
            {
                int num = timeSpan.Milliseconds / 10;
                __result = string.Format("{0:00}:{1:00}.{2:00}", (object)(timeSpan.Minutes + timeSpan.Hours * 60 + timeSpan.Days * 1440), (object)timeSpan.Seconds, (object)num);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
