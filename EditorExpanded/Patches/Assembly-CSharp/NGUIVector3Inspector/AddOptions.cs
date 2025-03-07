using HarmonyLib;
using System.Collections.Generic;

namespace EditorExpanded.Patches
{
	//NoEditorNumberLimit
	[HarmonyPatch(typeof(NGUIVector3Inspector), "AddOptions")]
	internal static class NGUIVector3Inspector__AddOptions
	{
		[HarmonyPostfix]
		internal static void Postfix(NGUIVector3Inspector __instance)
		{
			if (Mod.RemoveNumberLimits.Value)
			{
				List<UIExNumericInput> inputs = new List<UIExNumericInput>(){
				__instance.inputX_,
				__instance.inputY_,
				__instance.inputZ_
				};

				foreach (var input in inputs)
				{
					input.Min_ = float.MinValue;
					input.Max_ = float.MaxValue;
				}
			}
		}
	}
}
