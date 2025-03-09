using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EditorExpanded.Patches
{
    [HarmonyPatch(typeof(GUtils), "GetExportedTypesOfType")]
    internal static class GUtils__GetExportedTypesOfType
    {
        [HarmonyPostfix]
        internal static void Postfix(Type baseType, ref Type[] __result)
        {
            List<Type> types = __result.ToList();

            if (TypeExportManager.Types.Contains(baseType))
            {
                types.AddRange(TypeExportManager.GetTypesOfType(baseType));
            }

            __result = types.ToArray();
        }
    }
}
