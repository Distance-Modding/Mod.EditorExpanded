using HarmonyLib;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(LibraryTab), "Start")]
	internal static class LibraryTab__Start
	{
		[HarmonyPrefix]
		internal static bool Prefix(LibraryTab __instance)
		{
			__instance.iconSizeSlider_.onChange.Add(new EventDelegate(() => Mod.EditorIconSize.Value = __instance.IconSize_));

			__instance.iconSize_ = Mod.EditorIconSize.Value;

			__instance.rootFileData_ = G.Sys.ResourceManager_.LevelPrefabFileInfosRoot_;

			if (!Mod.DevFolderEnabled.Value && !G.Sys.GameManager_.IsDevBuild_)
			{
				__instance.rootFileData_.RemoveAllChildInfos((LevelPrefabFileInfo x) => x.IsDirectory_ && x.Name_ == "Dev");
			}

			__instance.currentDirectory_ = __instance.rootFileData_;
			__instance.iconSizeSlider_.value = UnityEngine.Mathf.InverseLerp(32f, 256f, __instance.iconSize_);
			__instance.searchInput_ = __instance.GetComponentInChildren<UIExInput>();
			__instance.StartCoroutine(__instance.CreateIconsAfterAFrame());

			return false;
		}
	}
}
