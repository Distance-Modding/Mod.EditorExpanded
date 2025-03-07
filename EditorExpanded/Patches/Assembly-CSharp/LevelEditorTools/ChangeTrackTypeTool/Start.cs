using HarmonyLib;
using LevelEditorTools;
using System.Collections.Generic;

namespace EditorExpanded.Patches
{
    //Editor Annihilator - Add all spline types to Change Track Type tool
    [HarmonyPatch(typeof(ChangeTrackTypeTool), "Start")]
    internal static class ChangeTrackTypeTool__Start
    {
        [HarmonyPostfix]
        internal static void Postfix()
        {
            if (Mod.IncludeAllSplinesInChangeTrackType.Value)
            {
                ChangeTrackTypeTool.roadEntries_ = new KeyValuePair<string, string>[34]
                {
                  ChangeTrackTypeTool.Folder("Roads", ChangeTrackTypeTool.selectedFolderColor_),
                  ChangeTrackTypeTool.Folder("Shapes", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Decorations", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Utility", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.KVP("Ancient Road", "SplineRoad"),
                  ChangeTrackTypeTool.KVP("Ancient Road Sideless", "AncientSidelessRoad"),

                  ChangeTrackTypeTool.KVP("Empire Road", "EmpireSplineRoad"),
                  ChangeTrackTypeTool.KVP("Empire Road Sideless", "EmpireSplineRoadSideless"),

                  ChangeTrackTypeTool.KVP("Glass Road", "GlassSplineRoad"),
                  ChangeTrackTypeTool.KVP("Glass Road 2", "GlassSplineRoad002"),
                  ChangeTrackTypeTool.KVP("Glass Road Sideless", "GlassSplineRoadSideless"),
                  ChangeTrackTypeTool.KVP("Glass Road Sideless 2", "GlassSplineRoadSideless002"),

                  ChangeTrackTypeTool.KVP("Virus Road", "VirusSplineRoad"),
                  ChangeTrackTypeTool.KVP("Virus Road Sideless", "VirusSplineSidelessRoad"),

                  ChangeTrackTypeTool.KVP("Nitronic Road", "NitronicSplineRoadStraight"),
                  ChangeTrackTypeTool.KVP("Nitronic Sideless Road", "NitronicSidelessSplineRoadStraight 1"),
                  ChangeTrackTypeTool.KVP("Nitronic Glass Road", "NitronicGlassSplineRoadStraight"),
                  ChangeTrackTypeTool.KVP("Nitronic Glass Sideless Road", "NitronicGlassSidelessSplineRoadStraight"),
                  ChangeTrackTypeTool.KVP("Nitronic Core Road", "NitronicCorePanelSidelessSplineRoadStraight"),
                  ChangeTrackTypeTool.KVP("Nitronic Platform Road", "NitronicPlatformPiece"),

                  ChangeTrackTypeTool.KVP("Golden Road", "SplineRoadSimple"),
                  ChangeTrackTypeTool.KVP("Golden Tunnel", "TunnelFlat"),

                  ChangeTrackTypeTool.KVP("Empire Transit Wall", "EmpireTransitWall"),
                  ChangeTrackTypeTool.KVP("Empire Transit Window 1", "EmpireTransitWindow001"),
                  ChangeTrackTypeTool.KVP("Empire Transit Window 2", "EmpireTransitWindow002"),
                  ChangeTrackTypeTool.KVP("Empire Transit Half Wall", "EmpireTransitHalfWall"),
                  ChangeTrackTypeTool.KVP("Empire Transit Half Window", "EmpireTransitHalfWindow"),
                  ChangeTrackTypeTool.KVP("Empire Pipe Tunnel", "EmpirePipeTunnel"),
                  ChangeTrackTypeTool.KVP("Empire Tunnel", "EmpireTunnel"),
                  ChangeTrackTypeTool.KVP("Empire Tunnel 2", "EmpireTunnel2"),
                  ChangeTrackTypeTool.KVP("Empire Tunnel 3", "EmpireTunnel3"),
                  ChangeTrackTypeTool.KVP("Virus Tunnel", "VirusTunnel"),
                  ChangeTrackTypeTool.KVP("Sky Islands Tunnel", "SkyIslandsTunnel01"),
                  ChangeTrackTypeTool.KVP("Nitronic Tunnel", "NitronicEchoTunnel")
                };
                ChangeTrackTypeTool.shapeEntries_ = new KeyValuePair<string, string>[24]
                {
                  ChangeTrackTypeTool.Folder("Roads", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Shapes", ChangeTrackTypeTool.selectedFolderColor_),
                  ChangeTrackTypeTool.Folder("Decorations", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Utility", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.KVP("Cylinder", "SplineCylinder"),
                  ChangeTrackTypeTool.KVP("Hexagon", "SplineHexagon"),
                  ChangeTrackTypeTool.KVP("Square", "SplineQuad"),
                  ChangeTrackTypeTool.KVP("Scales", "SplineScales"),
                  ChangeTrackTypeTool.KVP("Girder 2D", "SplineGirder2D"),
                  ChangeTrackTypeTool.KVP("Girder 3D", "SplineGirder3D"),
                  ChangeTrackTypeTool.KVP("Golden Wires 1", "SplineWires1"),
                  ChangeTrackTypeTool.KVP("Golden Wires 2", "SplineWirse2"),
                  ChangeTrackTypeTool.KVP("Quarter Pipe", "SplineQPipe"),
                  ChangeTrackTypeTool.KVP("40 Pipe", "Spline40Pipe"),
                  ChangeTrackTypeTool.KVP("Half Pipe", "SplineHalfPipe"),
                  ChangeTrackTypeTool.KVP("Quarter Tube", "SplineQTube"),
                  ChangeTrackTypeTool.KVP("Half Tube", "SplineHalfTube"),
                  ChangeTrackTypeTool.KVP("Tube", "SplineTube"),
                  ChangeTrackTypeTool.KVP("Square Tube", "SplineQuadTube"),
                  ChangeTrackTypeTool.KVP("Hexagon Tube", "SplineHexTube"),
                  ChangeTrackTypeTool.KVP("Tape", "SplineTape"),
                  ChangeTrackTypeTool.KVP("Cylinder HD", "SplineCylinderHD"),
                  ChangeTrackTypeTool.KVP("Golden Road", "SplineRoadSimple"),
                  ChangeTrackTypeTool.KVP("Golden Tunnel", "TunnelFlat")
                };
                ChangeTrackTypeTool.decorationEntries_ = new KeyValuePair<string, string>[28]
                {
                  ChangeTrackTypeTool.Folder("Roads", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Shapes", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Decorations", ChangeTrackTypeTool.selectedFolderColor_),
                  ChangeTrackTypeTool.Folder("Utility", ChangeTrackTypeTool.folderColor_),

                  ChangeTrackTypeTool.KVP("TrafficPlane", "TrafficPlane (Slow)"),
                  ChangeTrackTypeTool.KVP("Traffic Plane (not slow)", "TrafficPlane"),
                  ChangeTrackTypeTool.KVP("TrafficTube", "TrafficTube (Slow)"),
                  ChangeTrackTypeTool.KVP("Traffic Tube (not slow)", "TrafficTube"),
                  ChangeTrackTypeTool.KVP("Traffic Tube Plane", "TrafficTubePlane"),
                  ChangeTrackTypeTool.KVP("Traffic Plane Nitronic", "TrafficPlaneNitronic"),
                  ChangeTrackTypeTool.KVP("Wire", "ElectroPulseWire"),
                  ChangeTrackTypeTool.KVP("Wire (Thick)", "ElectroPulseWireThick"),
                  ChangeTrackTypeTool.KVP("Wire (Animated)", "ElectroPulseWireAnimated"),
                  ChangeTrackTypeTool.KVP("Nitronic Wire 1", "NitronicSplineWire01"),
                  ChangeTrackTypeTool.KVP("Nitronic Wire 2", "NitronicSplineWire02"),
                  ChangeTrackTypeTool.KVP("Nitronic Wire 3", "NitronicSplineWire03"),
                  ChangeTrackTypeTool.KVP("Nitronic Wire 4", "NitronicSplineWire04"),
                  ChangeTrackTypeTool.KVP("Nitronic Wire 5", "NitronicSplineWire05"),
                  ChangeTrackTypeTool.KVP("Nitronic Wire (Animated)", "Wire (Animated)"),

                  ChangeTrackTypeTool.KVP("Vine Leaf High", "VineLeafHi"),
                  ChangeTrackTypeTool.KVP("Vine Leaf Medium", "VineLeafMid"),
                  ChangeTrackTypeTool.KVP("Vine Leaf Low", "VineLeafLow"),
                  ChangeTrackTypeTool.KVP("Vine Leaf Sparse High", "VineLeafSparseHi"),
                  ChangeTrackTypeTool.KVP("Vine Leaf Sparse Medium", "VineLeafSparseMid"),
                  ChangeTrackTypeTool.KVP("Vine Leaf Sparse Low", "VineLeafSparseLow"),
                  ChangeTrackTypeTool.KVP("Vine Leaf Stagger Low", "VineLeafStaggerLow"),

                  ChangeTrackTypeTool.KVP("Halcyon Wall Edge Spline", "HalcyonWallEdgeSpline"),
                  ChangeTrackTypeTool.KVP("Halcyon Wall Spline", "HalcyonWallSpline")
                };
                ChangeTrackTypeTool.utilityEntries_ = new KeyValuePair<string, string>[6]
                {
                  ChangeTrackTypeTool.Folder("Roads", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Shapes", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Decorations", ChangeTrackTypeTool.folderColor_),
                  ChangeTrackTypeTool.Folder("Utility", ChangeTrackTypeTool.selectedFolderColor_),
                  ChangeTrackTypeTool.KVP("Invisible Spline", "InvisibleSpline"),
                  ChangeTrackTypeTool.KVP("Killgrid Spline", "KillGridSplineCylinder")
                };
            }
        }
    }
}
