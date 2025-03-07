using HarmonyLib;

namespace EditorExpanded.Patches
{
	//NoEditorNumberLimit
	[HarmonyPatch(typeof(UIExGenericNumericInput<float>), "Max_", MethodType.Setter)]
	internal static class UIExGenericNumericInput__Max__set
	{
		[HarmonyPrefix]
		internal static void Prefix(UIExGenericNumericInput<float> __instance)
		{
			if (Mod.RemoveNumberLimits.Value)
			{
				if (__instance is UIExNumericInput uiExNumericInput)
				{
					uiExNumericInput.max_ = float.MaxValue;
				}
			}
		}
	}
}
