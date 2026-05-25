using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerInitialMarker))]
public class PlayerInitialMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(PlayerInitialMarker point, GizmoType gizmo)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(point.transform.position, 1f);
    }
}