using HarmonyLib;
using System;

namespace EditorExpanded.Patches
{
    //Editor Annihilator
    [HarmonyPatch(typeof(LevelSettings), "NGUIVisitMedalTime")]
    internal static class LevelSettings__NGUIVisitMedalTime
    {
        [HarmonyPrefix]
        internal static bool Prefix(ref LevelSettings __instance, IVisitor visitor, string timeName, ref float time)
        {
            if (Mod.UnlimitedMedalTimes.Value)
            {
                string str = "";

                if (float.IsNegativeInfinity(time))
                {
                    str = "-I";
                }
                else if (float.IsPositiveInfinity(time))
                {
                    str = "+I";
                }
                else if (float.IsNaN(time))
                {
                    str = "NaN";
                }
                else if (time < 0)
                {
                    str = "-" + __instance.MedalTimeSpanToString(TimeSpan.FromMilliseconds((double)UnityEngine.Mathf.Abs(time)));
                }
                else
                {
                    str = __instance.MedalTimeSpanToString(TimeSpan.FromMilliseconds((double)UnityEngine.Mathf.Max(time, 0.0f)));
                }

                string val = str;
                visitor.Visit(timeName, ref val, LevelSettings.medalTimeOptions_);
                TimeSpan span;
                if (!(!(val != str) || !__instance.MedalTimeSpanTryParse(val, out span)))
                {
                    time = span.TotalMilliseconds <= 0.0 ? 0.0f : (float)span.TotalMilliseconds;
                }
                else if (!(!(val != str)) && val.StartsWith("-") && __instance.MedalTimeSpanTryParse(val.Substring(1, val.Length - 1), out span))
                {
                    time = -(float)span.TotalMilliseconds;
                }
                else if ((val != str) && val.StartsWith("$") && val.EndsWith("$") && val.Length > 1)
                {
                    try
                    {
                        time = float.Parse(val.Substring(1, val.Length - 2));
                    }
                    catch
                    {
                        time = float.NaN;
                    }
                }
                else if (!(!(val != str)) && (val.StartsWith("I") || val.StartsWith("+I")))
                {
                    time = float.PositiveInfinity;
                }
                else if (!(!(val != str)) && val.StartsWith("-I"))
                {
                    time = float.NegativeInfinity;
                }
                else if (!(!(val != str)) && val.StartsWith("N"))
                {
                    time = float.NaN;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
