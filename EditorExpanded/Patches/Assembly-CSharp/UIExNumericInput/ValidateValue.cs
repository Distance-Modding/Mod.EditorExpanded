using HarmonyLib;

namespace EditorExpanded.Patches
{
	//NoEditorNumberLimit
	[HarmonyPatch(typeof(UIExNumericInput), "ValidateValue")]
	internal static class UIExNumericInput__ValidateValue
	{
		[HarmonyPrefix]
		internal static bool Prefix(ref float __result, float val)
		{
			if (Mod.RemoveNumberLimits.Value)
			{
				if (float.IsNaN(val))
				{
					__result = 0;
				}
				else
				{
					__result = val;
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
