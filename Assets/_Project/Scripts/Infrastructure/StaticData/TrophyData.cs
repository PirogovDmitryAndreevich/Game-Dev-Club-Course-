using System;
using UnityEngine;

[Serializable]
public class TrophyData
{
    public Vector2 Position;

    public TrophyData(Vector2 position)
    {
        Position = position;
    }
}