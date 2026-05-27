using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public string LevelKey;

    public SceneID ID;
    public SceneID NextSceneID;

    public PlayerInitialData PlayerInitial;

    public List<LockData> Locks;
    public List<TrophyData> Trophies;
    public List<MedKitData> MedKits;
    public List<DefenseData> Defenses;
    public List<EnemySpawnerData> EnemySpawnerDatas;

    public LevelData()
    {
        Locks = new List<LockData>();
        Trophies = new List<TrophyData>();
        MedKits = new List<MedKitData>();
        EnemySpawnerDatas = new List<EnemySpawnerData>();
        PlayerInitial = new PlayerInitialData();
    }
}
