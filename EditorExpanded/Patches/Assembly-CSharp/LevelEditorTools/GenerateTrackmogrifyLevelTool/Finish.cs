using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(GenerateTrackmogrifyLevelTool), "Finish")]
	internal static class GenerateTrackmogrifyLevelTool__Finish
	{
		[HarmonyPrefix]
		internal static void Prefix()
		{
			EditorUtil.ClearQuickMemory();
		}
	}
}
