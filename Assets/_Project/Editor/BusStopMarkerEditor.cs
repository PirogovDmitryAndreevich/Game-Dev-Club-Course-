using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BusStopMarker))]
public class BusStopMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(BusStopMarker busStop, GizmoType gizmo)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(busStop.transform.position, 1f);
    }
}