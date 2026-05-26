using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MedKitInitialMarker))]
public class MedKitInitialMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(MedKitInitialMarker medKit, GizmoType gizmo)
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(medKit.transform.position, 0.5f);
    }
}
