using HarmonyLib;
using System.Collections.Generic;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(LevelEditorMusicTrackSelectMenuLogic), "GenerateMusicNameList")]
	internal static class LevelEditorMusicTrackSelectMenuLogic__GenerateMusicNameList
	{
		[HarmonyPostfix]
		internal static void Postfix(LevelEditorMusicTrackSelectMenuLogic __instance)
		{
			if (Mod.AdvancedMusicSelection.Value && !G.Sys.GameManager_.IsDevBuild_)
			{
				__instance.buttonList_.Clear();
				List<AudioManager.MusicCue> music = G.Sys.AudioManager_.MusicCues_;

				if (!Mod.AdvancedMusicSelection.Value)
				{
					music.RemoveAll(x => x.devEvent_);
				}

				__instance.CreateButtons(music, UnityEngine.Color.white);
				__instance.buttonList_.SortAndUpdateVisibleButtons();
			}
		}
	}
}
