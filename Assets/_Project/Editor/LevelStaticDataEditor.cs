using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelStaticData levelData = (LevelStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            string sceneKey = SceneManager.GetActiveScene().name;

            if (levelData.LevelData == null)
                levelData.LevelData = new List<LevelData>();

            LevelData data = levelData.LevelData
                .FirstOrDefault(x => x.LevelKey == sceneKey);

            if (data == null)
            {
                data = new LevelData();
                data.LevelKey = sceneKey;

                levelData.LevelData.Add(data);
            }

            if(data.PlayerInitial == null)
                data.PlayerInitial = new PlayerInitialData();

            data.PlayerInitial.Position = FindObjectOfType<PlayerInitialMarker>()
                .transform.position;

            data.EnemySpawnerDatas = FindObjectsOfType<EnemySpawnMarker>()
                .Select(x => new EnemySpawnerData(
                    x.GetComponent<UniqueId>().Id,
                    x.TypeID,
                    x.transform.position,
                    x.WayPoints.Select(y => new WayPointData(y.transform.position)).ToList()))
                .ToList();

            EditorUtility.SetDirty(target);
        }
    }
}