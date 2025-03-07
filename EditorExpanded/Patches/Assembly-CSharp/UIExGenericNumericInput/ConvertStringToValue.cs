/*using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Editor Decimal Precision
    [HarmonyPatch(typeof(UIExGenericNumericInput<float>), "ConvertStringToValue")]
    internal static class UIExGenericNumericInput__ConvertStringToValue
    {
        [HarmonyPostfix]
        internal static void Postfix(ref float __result, UIExGenericNumericInput<float> __instance, string s)
        {
            __instance.expressionTree_ = new ExpressionTree(s);
            double num = __instance.expressionTree_.Evaluate(__instance.ConvertValueToDouble(__instance.PreviousValue_), true);
            if (double.IsNaN(num))
            {
                __instance.expressionTree_ = (ExpressionTree)null;
                __result = float.NaN;
            }
            else
            {
                __result = __instance.ValidateValue(__instance.ConvertDoubleToValue(num));
            }

        }
    }
}*/
