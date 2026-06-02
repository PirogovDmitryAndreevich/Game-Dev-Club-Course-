using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnerData
{
    public string Id;
    public EnemyTypeId TypeId;
    public Vector2 Position;
    public List<WayPointData> WayPoints;

    public EnemySpawnerData(string id, EnemyTypeId typeId, Vector2 position, List<WayPointData> wayPoints)
    {
        Id = id;
        TypeId = typeId;
        Position = position;
        WayPoints = wayPoints;
    }
}
