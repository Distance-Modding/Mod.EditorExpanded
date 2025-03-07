using HarmonyLib;
using System.Linq;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(LevelEditorLevelNameSelectMenuLogic), "GenerateLevelNameList")]
	internal static class LevelEditorLevelNameSelectMenuLogic__GenerateLevelNameList
	{
		[HarmonyPostfix]
		internal static void Postfix(LevelEditorLevelNameSelectMenuLogic __instance)
		{
			if (Mod.DisplayWorkshopLevels.Value && !G.Sys.GameManager_.IsDevBuild_)
			{
				LevelSetsManager levelSets = G.Sys.LevelSets_;

				__instance.CreateButtons(levelSets.LevelsLevelFilePaths_.ToList(), Colors.YellowColors.gold, LevelEditorLevelNameSelectMenuLogic.LevelPathEntry.DisplayOption.RelativePath);
				__instance.CreateButtons(levelSets.WorkshopLevelFilePaths_.ToList(), GConstants.communityLevelColor_, LevelEditorLevelNameSelectMenuLogic.LevelPathEntry.DisplayOption.LevelName);

				__instance.buttonList_.SortAndUpdateVisibleButtons();
			}
		}
	}
}
