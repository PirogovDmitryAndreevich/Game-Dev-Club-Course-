using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public string LevelKey;

    public SceneID ID;
    public SceneID NextSceneID;

    public PlayerInitialData PlayerInitial;

    public List<EnemySpawnerData> EnemySpawnerDatas;
}
