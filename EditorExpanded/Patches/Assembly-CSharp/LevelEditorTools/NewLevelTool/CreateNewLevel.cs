using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(NewLevelTool), "CreateNewLevel")]
	internal static class NewLevelTool__CreateNewLevel
	{
		[HarmonyPrefix]
		internal static void Prefix()
		{
			EditorUtil.ClearQuickMemory();
		}
	}
}
