using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPointMarker))]
public class WayPointMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(WayPointMarker point, GizmoType gizmo)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(point.transform.position, 0.5f);
    }
}