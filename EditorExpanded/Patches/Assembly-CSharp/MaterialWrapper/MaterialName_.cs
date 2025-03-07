﻿using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Sub Materials
    [HarmonyPatch(typeof(MaterialWrapper), "MaterialName_", MethodType.Getter)]
    internal static class MaterialWrapper__MaterialName_
    {
        [HarmonyPostfix]
        internal static void Postfix(ref string __result, MaterialWrapper __instance)
        {
            if (Mod.EnableSubTextures.Value)
            {
                __result = __instance.matInfo_.matName_ + __instance.materialIndex_ + __instance.renderer_.GetInstanceID();
            }
        }
    }
}
