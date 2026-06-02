using System;
using UnityEngine;

[Serializable]
public class BusData
{
    public Vector2 SpawnPosition;
    public Vector2 EndMovePoint;
    public bool IsRight;

    public BusData(Vector2 spawnPosition, Vector2 endMovePoint, bool isRight)
    {
        SpawnPosition = spawnPosition;
        EndMovePoint = endMovePoint;
        IsRight = isRight;
    }
}