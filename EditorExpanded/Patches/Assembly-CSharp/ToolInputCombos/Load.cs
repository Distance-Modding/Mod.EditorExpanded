using HarmonyLib;
using LevelEditorTools;
using System;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace EditorExpanded.Patches
{
	//Input combos for custom keyboard shortcuts
	[HarmonyPatch(typeof(ToolInputCombos), "Load")]
	internal static class ToolInputCombos__Load
	{
		[HarmonyPostfix]
		internal static void Postfix(ref ToolInputCombos __result, ref string fileName)
		{
			if (__result is null)
			{
				return;
			}

			switch (fileName)
			{
				case "BlenderToolInputCombos":  // Scheme A
					AddCustomHotkeys(ref __result, 'A');
					break;
				case "UnityToolInputCombos":    // Scheme B
					AddCustomHotkeys(ref __result, 'B');
					break;
			}
		}

		internal static void AddCustomHotkeys(ref ToolInputCombos __result, char scheme)
		{
			Assembly assemblyCS = typeof(G).Assembly;

			bool filterType(Type type)
			{
				return !ReferenceEquals(assemblyCS, type.Assembly)
					&& type.GetCustomAttribute<EditorToolAttribute>(false) is object;
			}

			foreach (Type toolType in TypeExportManager.GetTypesOfType(typeof(LevelEditorTool)).Where(filterType))
			{
				if (GetToolInfo(toolType) is ToolInfo info && toolType.GetCustomAttribute<EditorToolAttribute>(false) is KeyboardShortcutAttribute attribute)
				{
					__result.Add(attribute.Get(scheme).ToString(), info.Name_);
				}
			}
		}

		internal static ToolInfo GetToolInfo(Type toolType)
		{
			if (toolType is null)
			{
				return null;
			}

			const BindingFlags bindingAttr = Static | NonPublic;
			if (toolType.GetField("info_", bindingAttr) is FieldInfo info_)
			{
				return info_.GetValue(null) as ToolInfo;
			}

			LevelEditorTool instance = Activator.CreateInstance(toolType) as LevelEditorTool;
			return instance?.Info_;
		}
	}
}
