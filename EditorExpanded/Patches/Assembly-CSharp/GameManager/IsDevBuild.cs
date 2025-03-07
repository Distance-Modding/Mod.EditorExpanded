using HarmonyLib;

namespace EditorExpanded.Patches
{
	//Editor Additions - Enable Dev folder & Editor Annihilator - Remove Level Creator Field
	[HarmonyPatch(typeof(GameManager), "IsDevBuild_", MethodType.Getter)]
	internal static class GameManager__IsDevBuild_get
	{
		internal static bool Prefix(ref bool __result)
		{
			if (Mod.DevMode)
			{
				__result = true;
				return false;
			}

			return true;
		}

		[HarmonyPostfix]
		internal static void Postfix(ref bool __result)
		{
			if (!Mod.RemoveCreatorField.Value)
			{
				if (Mod.DevBuildForCreatorName)
				{
					__result = true;
				}
				Mod.DevBuildForCreatorName = false;
			}
		}
	}
}
