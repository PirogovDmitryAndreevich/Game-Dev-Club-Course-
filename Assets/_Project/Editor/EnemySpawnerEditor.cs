using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : UnityEditor.Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmo)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawner.transform.position, 1f);
    }
}
