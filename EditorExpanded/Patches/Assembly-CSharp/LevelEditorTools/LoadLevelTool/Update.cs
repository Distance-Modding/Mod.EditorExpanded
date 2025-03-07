using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(LoadLevelTool), "Update")]
	internal static class LoadLevelTool__Update
	{
		[HarmonyPrefix]
		internal static void Prefix()
		{
			EditorUtil.ClearQuickMemory();
		}
	}
}
