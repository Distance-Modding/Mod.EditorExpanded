using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace EditorExpanded.Patches
{
	//Editor Additions
	[HarmonyPatch(typeof(Group), "Visit")]
	internal static class Group__Visit
	{
		[HarmonyPrefix]
		internal static bool Prefix(Group __instance, IVisitor visitor)
		{
			if (!(visitor is Serializers.Serializer) && !(visitor is Serializers.Deserializer))
			{
				UnityEngine.GameObject[] SubObjects = __instance.gameObject.GetChildren();

				if (SubObjects.Length == 0)
				{
					visitor.VisualLabel("No child objects found!".Colorize(UnityEngine.Color.white));
					return false;
				}

				if (SubObjects.Length > 0)
				{
					visitor.VisualLabel("Group Hierarchy");

					int Index = 1;

					foreach (UnityEngine.GameObject Children in SubObjects)
					{
						string Name = Children.name;

						if (Children.HasComponent<CustomName>())
						{
							CustomName CustomNameComponent = Children.GetComponent<CustomName>();
							Name = $"[b]{CustomNameComponent.CustomName_}[/b]".Colorize(UnityEngine.Color.white);
						}

						visitor.VisitAction($"Inspect {Name} (#{Index})", () => EditorUtil.Inspect(Children), null);

						++Index;
					}
				}
			}

			return true;
		}

		//GroupCenterpointMover
		//This patch adds a translator to group centerpoints
		[HarmonyPostfix]
		internal static void Postfix(Group __instance, IVisitor visitor, ISerializable prefabComp)
		{
			if (Mod.EnableGroupCenterMover.Value)
			{
				if ((UnityEngine.Object)prefabComp == (UnityEngine.Object)null)
				{
					Vector3 transfoval = new Vector3(__instance.transform.localPosition.x, __instance.transform.localPosition.y, __instance.transform.localPosition.z);
					Quaternion rotoval = new Quaternion(__instance.transform.rotation.x, __instance.transform.rotation.y, __instance.transform.rotation.z, __instance.transform.rotation.w);
					List<Vector3> chileinittrans = new List<Vector3>();
					List<Quaternion> chileinitroto = new List<Quaternion>();
					foreach (GameObject groupchile in __instance.gameObject.GetChildren())
					{
						chileinittrans.Add(groupchile.transform.position);
						chileinitroto.Add(groupchile.transform.rotation);
					}

					visitor.Visit("Centerpoint Position", ref transfoval);
					visitor.Visit("Centerpoint Rotation", ref rotoval);
					Vector3 transfochange = transfoval - __instance.transform.localPosition;
					__instance.transform.localPosition = __instance.transform.localPosition + transfochange;
					__instance.transform.rotation = rotoval;
					int chileintitransindex = 0;
					foreach (GameObject groupchile in __instance.gameObject.GetChildren())
					{
						groupchile.transform.position = chileinittrans.ToArray()[chileintitransindex];
						groupchile.transform.rotation = chileinitroto.ToArray()[chileintitransindex];
						chileintitransindex++;
					}

					__instance.localBounds_ = Group.CalculateBoundsFromImmediateChildren(__instance);
				}
			}
		}
	}
}
