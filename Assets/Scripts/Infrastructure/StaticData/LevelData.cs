using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public string LevelKey;

    public SceneID ID;
    public SceneID NextSceneID;

    public PlayerInitialData PlayerInitial;

    public List<MedKitData> MedKits;
    public List<DefenseData> Defenses;
    public List<EnemySpawnerData> EnemySpawnerDatas;

    public LevelData()
    {
        MedKits = new List<MedKitData>();
        EnemySpawnerDatas = new List<EnemySpawnerData>();
        PlayerInitial = new PlayerInitialData();
    }
}