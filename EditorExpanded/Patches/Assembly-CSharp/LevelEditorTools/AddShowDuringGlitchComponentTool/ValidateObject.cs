using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Add Component to any object
    [HarmonyPatch(typeof(AddShowDuringGlitchComponentTool), "ValidateObject")]
    internal static class AddShowDuringGlitchComponentTool__ValidateObject
    {
        [HarmonyPostfix]
        internal static void Postfix(ref bool __result)
        {
            if (Mod.UnlimitedAddComponent.Value)
                __result = true;
        }
    }
}