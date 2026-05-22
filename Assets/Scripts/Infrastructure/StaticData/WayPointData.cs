using System;
using UnityEngine;

[Serializable]
public class WayPointData
{
    public Vector2 Position;

    public WayPointData(Vector2 position)
    {
        Position = position;
    }
}