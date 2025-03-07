using HarmonyLib;

namespace EditorExpanded.Patches
{
	//NoEditorNumberLimit (Possibly not needed)
	/*[HarmonyPatch(typeof(Options.MaxFloat), "Format")]
	internal static class Options_MaxFloat__Format
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref string __result)
		{
			__result = string.Empty;
			return false;
		}
	}

	[HarmonyPatch(typeof(Options.MinFloat), "Format")]
	internal static class Options_MinFloat__Format
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref string __result)
		{
			__result = string.Empty;
			return false;
		}
	}

	[HarmonyPatch(typeof(Options.MaxInt), "Format")]
	internal static class Options_MaxInt__Format
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref string __result)
		{
			__result = string.Empty;
			return false;
		}
	}

	[HarmonyPatch(typeof(Options.MinInt), "Format")]
	internal static class Options_MinInt__Format
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref string __result)
		{
			__result = string.Empty;
			return false;
		}
	}*/
}
