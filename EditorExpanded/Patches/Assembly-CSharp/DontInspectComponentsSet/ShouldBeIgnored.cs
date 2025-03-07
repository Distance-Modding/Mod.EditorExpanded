using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator 
    [HarmonyPatch(typeof(DontInspectComponents.Set), "ShouldBeIgnored")]
    internal static class DontInspectComponentsSet__ShouldBeIgnored
    {
        [HarmonyPostfix]
        internal static void Postfix(ref bool __result, UnityEngine.Component comp)
        {
            if (Mod.EnableHiddenComponent.Value)
            {
                if ((UnityEngine.Object)comp == (UnityEngine.Object)null)
                {
                    __result = true;
                }
                else
                {
                    __result = false;
                }
            }
        }
    }
}
