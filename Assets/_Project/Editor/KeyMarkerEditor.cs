using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyMarker))]
public class KeyMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(KeyMarker key, GizmoType gizmo)
    {
        Gizmos.color = key.Color;
        Gizmos.DrawSphere(key.transform.position, 0.5f);
    }
}
