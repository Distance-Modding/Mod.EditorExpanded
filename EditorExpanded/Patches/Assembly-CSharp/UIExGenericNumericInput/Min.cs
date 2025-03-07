using HarmonyLib;

namespace EditorExpanded.Patches
{
	//NoEditorNumberLimit
	[HarmonyPatch(typeof(UIExGenericNumericInput<float>), "Min_", MethodType.Setter)]
	internal static class UIExGenericNumericInput__Min__set
	{
		[HarmonyPrefix]
		internal static void Prefix(UIExGenericNumericInput<float> __instance)
		{
			if (Mod.RemoveNumberLimits.Value)
			{
				if (__instance is UIExNumericInput uiExNumericInput)
				{
					uiExNumericInput.min_ = float.MinValue;
				}
			}
		}
	}
}
