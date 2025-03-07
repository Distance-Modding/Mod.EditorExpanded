using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Sub Materials
    [HarmonyPatch(typeof(MaterialWrapper), "ComponentName_", MethodType.Getter)]
    internal static class MaterialWrapper__ComponentName_
    {
        [HarmonyPostfix]
        internal static void Postfix(ref string __result, MaterialWrapper __instance)
        {
            if (Mod.EnableSubTextures.Value)
            {
                __result = "Material: " + (__instance.matInfo_.matName_ + __instance.materialIndex_).Colorize(Colors.GreenColors.seaGreen);
            }
        }
    }
}
