using Events;
using Events.Stunt;
using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Fixing Stunt Collectible Object
    [HarmonyPatch(typeof(StuntCollectibleLogic), "Awake")]
    internal static class StuntCollectibleLogic__Awake
    {
        [HarmonyPrefix]
        internal static bool Prefix(StuntCollectibleLogic __instance)
        {
            StaticEvent<StuntCollectibleSpawned.Data>.Subscribe(new StaticEvent<StuntCollectibleSpawned.Data>.Delegate(__instance.OnEventStuntCollectibleSpawned));
            StaticEvent<HitTagStuntCollectible.Data>.Subscribe(new StaticEvent<HitTagStuntCollectible.Data>.Delegate(__instance.OnHitTagStuntCollectible));
            __instance.col_ = __instance.GetComponent<UnityEngine.SphereCollider>();
            __instance.col_.enabled = false;
            __instance.phantom_ = __instance.GetComponent<Phantom>();
            return false;
        }
    }
}
