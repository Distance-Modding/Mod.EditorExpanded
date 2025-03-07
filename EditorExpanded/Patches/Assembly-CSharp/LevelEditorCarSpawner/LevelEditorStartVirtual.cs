using HarmonyLib;
using System;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Multiple Car Spawners
    [HarmonyPatch(typeof(LevelEditorCarSpawner), "LevelEditorStartVirtual")]
    internal static class LevelEditorCarSpawner__LevelEditorStartVirtual
    {
        [HarmonyPrefix]
        internal static bool Prefix(LevelEditorCarSpawner __instance)
        {
            if (Mod.MultipleCarSpawners.Value)
            {
                LevelEditor levelEditor = G.Sys.LevelEditor_;
                foreach (LevelEditorCarSpawner editorCarSpawner in levelEditor.WorkingLevel_.FindComponentsOfType<LevelEditorCarSpawner>())
                {
                    if ((Object)editorCarSpawner != (Object)__instance)
                    {

                    }
                }
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
