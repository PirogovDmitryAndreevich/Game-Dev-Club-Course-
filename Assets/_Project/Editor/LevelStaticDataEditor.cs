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

            LevelData data = levelData.LevelData
                .FirstOrDefault(x => x.LevelKey == sceneKey);

            if (data == null)
            {
                data = new LevelData();
                data.LevelKey = sceneKey;

                levelData.LevelData.Add(data);
            }

            data.PlayerInitial.Position = FindObjectOfType<PlayerInitialMarker>()
                .transform.position;

            data.BusStop.Position = FindObjectOfType<BusStopMarker>().transform.position;

            data.Locks = FindObjectsOfType<LockInitialMarker>()
                .Select(x => new LockData(x.transform.position,x.Color, new KeyData(x.Key.transform.position)))
                .ToList();

            data.Trophies = FindObjectsOfType<TrophyInitialMarker>()
                .Select(x => new TrophyData(x.transform.position))
            .ToList();

            data.MedKits = FindObjectsOfType<MedKitInitialMarker>()
                .Select(x => new MedKitData(
                    x.transform.position,
                    x.Value))
                .ToList();

            data.Defenses = FindObjectsOfType<DefenseInitialMarker>()
                .Select(x => new DefenseData(
                    x.transform.position,
                    x.Value))
                .ToList();

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