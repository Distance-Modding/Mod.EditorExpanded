using HarmonyLib;

namespace EditorExpanded.Patches
{
    //Editor Additions
    [HarmonyPatch(typeof(LevelEditor), "UpdateOutlines")]
    internal static class LevelEditor__UpdateOutlines
    {
        [HarmonyPrefix]
        internal static bool Prefix(LevelEditor __instance)
        {
            int count = __instance.selectedObjects_.List_.Count;
            if (count != __instance.outlines_.Count)
            {
                Mod.Log.LogInfo($"Object Count: {count} Outline Count: {__instance.outlines_.Count}");
                Mod.Log.LogInfo($"List of selected Objects: ");
                foreach(UnityEngine.GameObject gObject in __instance.selectedObjects_.List_)
                {
                    Mod.Log.LogInfo(gObject.name);
                }
                UnityEngine.Debug.LogError((object)"Outlines Count is different than Selected Objects Count");
            }
            else
            {
                for (int index = 0; index < count; ++index)
                    __instance.UpdateOutlineColor(index);
            }

            return false;
        }
    }
}
