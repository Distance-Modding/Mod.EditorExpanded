using HarmonyLib;
using LevelEditorTools;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Add Component to any object
    [HarmonyPatch(typeof(AddSetActiveAfterWarpComponentTool), "ValidateObject")]
    internal static class AddSetActiveAfterWarpComponentTool__ValidateObject
    {
        [HarmonyPostfix]
        internal static void Postfix(ref bool __result)
        {
            if (Mod.UnlimitedAddComponent.Value)
                __result = true;
        }
    }
}