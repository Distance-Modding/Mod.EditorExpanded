using HarmonyLib;
using LevelEditorTools;
using System.Collections.Generic;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(SelectMusicTrackNameFromListTool), "AddEntries")]
	internal static class SelectMusicTrackNameFromListTool__AddEntries
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref Dictionary<string, string> entryList)
		{
			if (Mod.AdvancedMusicSelection.Value)
			{
				foreach (AudioManager.MusicCue music in G.Sys.AudioManager_.MusicCues_)
				{
					entryList.Add(music.displayName_, music.displayName_);
				}

				return false;
			}

			return true;
		}
	}
}
