using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Editor Decimal Precision
    [HarmonyPatch(typeof(UIExNumericInput), "ConvertValueToString")]
    internal static class UIExNumericInput__ConvertValueToString
    {
        [HarmonyPostfix]
        internal static void Postfix(ref string __result, float val)
        {
            System.String precisionStringer = "0.0";
            for (int i = 0; i < Mod.EditorDecimalPrecision.Value; i++)
            {
                precisionStringer = precisionStringer + "#";
            }
            __result = val.ToString(precisionStringer);
        }
    }
}
