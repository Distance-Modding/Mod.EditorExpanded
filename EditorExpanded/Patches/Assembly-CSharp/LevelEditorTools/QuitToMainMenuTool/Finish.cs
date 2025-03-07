using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(QuitToMainMenuTool), "Finish")]
	internal static class QuitToMainMenuTool__Finish
	{
		[HarmonyPrefix]
		internal static void Prefix()
		{
			EditorUtil.ClearQuickMemory();
		}
	}
}
