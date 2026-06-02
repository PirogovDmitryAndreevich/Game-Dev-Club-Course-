using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string LevelKey;
    public string Name;
    public Sprite Sprite;

    public SceneID ID;
    public SceneID NextSceneID;

    public PlayerInitialData PlayerInitial;
    public BusStopData BusStop;

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
