﻿#pragma warning disable IDE1006
using LevelEditorTools;

namespace EditorExpanded.Editor.Tools.Quickselect
{
	//Editor Additions
	public class SaveSelectionToolBase : InstantTool
	{
		internal static ToolInfo info_ => new ToolInfo("Save Selection", "Saves the inspected object into the memory.", ToolCategory.View, ToolButtonState.Button, true, 1100);
		public override ToolInfo Info_ => info_;

		public virtual int QuickAccessIndex => -1;

		public static void Register()
		{
		}

		public override bool Run()
		{
			var Editor = G.Sys.LevelEditor_;
			var Selection = Editor.activeObject_;

			if (Selection)
			{
				EditorUtil.SetQuickMemory(QuickAccessIndex, Selection);
			}
			else
			{
				MessageBox.Create("You must select only 1 object to use this tool.", "ERROR")
					.SetButtons(MessagePanelLogic.ButtonType.Ok)
					.Show();
			}

			return true;
		}
	}
}
