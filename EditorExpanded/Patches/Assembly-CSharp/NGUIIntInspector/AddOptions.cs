using HarmonyLib;

namespace EditorExpanded.Patches
{
	//NoEditorNumberLimit
	[HarmonyPatch(typeof(NGUIIntInspector), "AddOptions")]
	internal static class NGUIIntInspector__AddOptions
	{
		[HarmonyPostfix]
		internal static void Postfix(NGUIIntInspector __instance)
		{
			if (Mod.RemoveNumberLimits.Value)
			{
				// REWRITE VALUES AFTER ORIGINAL CODE ANYWAY :^)

				__instance.SetMin(int.MinValue);
				__instance.SetMax(int.MaxValue);

				// "SHOULD" WORK
			}
		}
	}
}
