using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DefenseInitialMarker))]
public class DefenseInitialMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(DefenseInitialMarker defense, GizmoType gizmo)
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(defense.transform.position, 0.5f);
    }
}