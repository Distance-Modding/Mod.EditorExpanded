using HarmonyLib;
using LevelEditorTools;
using System;
using System.Reflection;

//Hey so this breaks at the Activator.CreateInstance line.

// [Error  : Unity Log] NullReferenceException: Object reference not set to an instance of an object
// Stack trace:
// LevelEditorTools.TestAnimationTool..ctor ()
// System.Reflection.MonoCMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture)
// Rethrow as TargetInvocationException: Exception has been thrown by the target of an invocation.
// System.Reflection.MonoCMethod.Invoke (object,System.Reflection.BindingFlags,System.Reflection.Binder,object[],System.Globalization.CultureInfo) <0x0027b>
// System.Reflection.MonoCMethod.Invoke (System.Reflection.BindingFlags,System.Reflection.Binder,object[],System.Globalization.CultureInfo) <0x00039>
// System.Reflection.ConstructorInfo.Invoke (object[]) <0x00053>
// System.Activator.CreateInstance (System.Type,bool) <0x00233>
// System.Activator.CreateInstance (System.Type) <0x0001e>
// EditorExpanded.Patches.ToolInputCombos__Load.AddCustomHotkeys (ToolInputCombos&,char) <0x000a2>
// EditorExpanded.Patches.ToolInputCombos__Load.Postfix (ToolInputCombos&,string&) <0x0008d>
// (wrapper dynamic-method) ToolInputCombos.DMD<ToolInputCombos..Load> (string) <0x000c3>
// LevelEditor.set_CurrentInputScheme_ (string) <0x0007c>
// LevelEditor.Start () <0x001b3>

//WAI


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
                Mod.Log.LogInfo("NULL!??!?!");
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

            foreach (Type toolType in TypeExportManager.GetTypesOfType(typeof(LevelEditorTool)))
            {
                Mod.Log.LogInfo(toolType.FullName);
                if (ReferenceEquals(assemblyCS, toolType.Assembly))
                {
                    Mod.Log.LogWarning("Ignoring");
                    continue;
                }

                LevelEditorTool instance = Activator.CreateInstance(toolType) as LevelEditorTool;

                if (toolType.HasAttribute<EditorToolAttribute>() && toolType.GetAttribute(out KeyboardShortcutAttribute attribute, false))
                {
                    if (instance.Info_ is null)
                        Mod.Log.LogInfo(toolType.FullName + " Info is null");

                    __result.Add(attribute.Get(scheme).ToString(), instance.Info_.Name_);
                }
            }
        }
    }
}
