using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Fixing Stunt Bubble
    [HarmonyPatch(typeof(StuntBubbleLogic), "Awake")]
    internal static class StuntBubbleLogic__Awake
    {
        [HarmonyPrefix]
        internal static bool Prefix(StuntBubbleLogic __instance)
        {

            __instance.col_ = __instance.GetComponent<UnityEngine.SphereCollider>();
            __instance.color_ = __instance.bubbleRend_.material.GetColor("_Color");
            __instance.alpha_ = __instance.color_.a;
            __instance.emitColor_ = __instance.bubbleRend_.material.GetColor("_EmitColor");
            __instance.emitAlpha_ = __instance.emitColor_.a;
            __instance.outlineColor_ = __instance.bubbleOutlineRend_.material.GetColor("_Color");
            __instance.scale_ = __instance.transform.localScale;
            __instance.SetAlpha(0.0f);
            return false;
        }
    }
}
