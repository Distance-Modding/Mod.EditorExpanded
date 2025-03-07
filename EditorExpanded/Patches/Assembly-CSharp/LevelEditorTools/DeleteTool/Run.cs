using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(DeleteTool), "Run")]
	internal static class DeleteTool__Run
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref bool __result)
		{
			if (!EditorUtil.IsSelectionRoot())
			{
				EditorUtil.PrintToolInspectionStackError();
				__result = false;

				return false;
			}

			return true;
		}
	}
}
