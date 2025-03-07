using HarmonyLib;

namespace EditorExpanded.Patches
{
	//NoEditorNumberLimit
	[HarmonyPatch(typeof(NGUIFloatInspector), "AddOptions")]
	internal static class NGUIFloatInspector__AddOptions
	{
		[HarmonyPostfix]
		internal static void Postfix(NGUIFloatInspector __instance)
		{
			if (Mod.RemoveNumberLimits.Value)
			{
				// REWRITE VALUES AFTER ORIGINAL CODE ANYWAY :^)

				__instance.SetMin(float.MinValue);
				__instance.SetMax(float.MaxValue);

				// "SHOULD" WORK
			}
		}
	}
}
