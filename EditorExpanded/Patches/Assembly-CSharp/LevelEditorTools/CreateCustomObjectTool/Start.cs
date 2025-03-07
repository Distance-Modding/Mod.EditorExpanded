using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
	//Editor Additions - Prevents Inspect Group Error
	[HarmonyPatch(typeof(CreateCustomObjectTool), "Start")]
	internal static class CreateCustomObjectTool__Start
	{
		internal static bool Prefix()
		{
			if (!EditorUtil.IsSelectionRoot())
			{
				EditorUtil.PrintToolInspectionStackError();

				return false;
			}

			return true;
		}
	}
}
