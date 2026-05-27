using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrophyInitialMarker))]
public class TrophyInitialMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(TrophyInitialMarker trophy, GizmoType gizmo)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(trophy.transform.position, 0.5f);
    }
}