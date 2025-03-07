using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Remove Level Creator Field
    [HarmonyPatch(typeof(LevelSettings), "Visit")]
    internal static class LevelSettings__Visit
    {
        [HarmonyPrefix]
        internal static void Prefix(LevelSettings __instance, IVisitor visitor, ISerializable prefabComp, int version)
        {
            if (!Mod.RemoveCreatorField.Value)
            {
                Mod.DevBuildForCreatorName = true;
            }
        }
    }
}
