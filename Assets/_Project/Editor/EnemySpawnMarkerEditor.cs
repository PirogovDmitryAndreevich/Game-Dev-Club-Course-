using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawnMarker))]
public class EnemySpawnMarkerEditor : Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(EnemySpawnMarker spawner, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawner.transform.position, 1f);
    }
}
