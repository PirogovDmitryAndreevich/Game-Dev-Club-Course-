using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ForBusMarker))]
public class BusSpawnMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(ForBusMarker bus, GizmoType gizmo)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(bus.transform.position, 0.5f);
    }
}