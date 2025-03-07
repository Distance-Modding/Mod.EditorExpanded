using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Fixing Credit Orb
    [HarmonyPatch(typeof(CreditsNameOrbLogic), "Visit")]
    internal static class CreditsNameOrbLogic__Visit
    {
        [HarmonyPrefix]
        internal static bool Prefix(CreditsNameOrbLogic __instance, IVisitor visitor, ISerializable prefabComp, int version)
        {
            if (version == 0)
            {

            }
            else
            {
                visitor.Visit("name_", ref __instance.name_);
                if (visitor is ISerializer)
                {
                    visitor.Visit("key_", ref __instance.key_);
                    visitor.Visit("linkedKey_", ref __instance.linkedKey_);
                }
            }
            if (!__instance.initialized_ && !G.Sys.GameManager_.IsLevelEditorMode_)
            {
                //Mod.Log.LogInfo("BOOYAE");
                //__instance.Initialize();
            }
            return false;
        }
    }
}
