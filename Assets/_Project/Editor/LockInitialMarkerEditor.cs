using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LockInitialMarker))]
public class LockInitialMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(LockInitialMarker lockMarker, GizmoType gizmo)
    {
        lockMarker.Key.Color = lockMarker.Color;
        Gizmos.color = lockMarker.Color;
        Gizmos.DrawCube(lockMarker.transform.position, lockMarker.Size);
    }
}