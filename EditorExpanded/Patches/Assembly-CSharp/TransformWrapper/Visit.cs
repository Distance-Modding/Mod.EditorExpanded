﻿using HarmonyLib;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(TransformWrapper), "Visit")]
	internal static class TransformWrapper__Visit
	{
		[HarmonyPrefix]
		internal static void Prefix(TransformWrapper __instance, IVisitor visitor)
		{
			if (!(visitor is Serializers.Serializer) && !(visitor is Serializers.Deserializer))
			{
				UnityEngine.Transform ObjectTransform = __instance.tComponent_;

				if (!ObjectTransform.IsRoot())
				{
					visitor.VisitAction("Inspect Parent", () =>
					{
						var Editor = G.Sys.LevelEditor_;
						var Selection = Editor.SelectedObjects_;

						if (Selection.Count == 1)
						{
							EditorUtil.Inspect(ObjectTransform.parent.gameObject);
						}
						else
						{
							MessageBox.Create("You must select only 1 object to use this tool.", "ERROR")
							.SetButtons(MessagePanelLogic.ButtonType.Ok)
							.Show();
						}
					}, null);
				}
			}
		}
	}
}
