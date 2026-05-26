using System;
using UnityEngine;

[Serializable]
public class DefenseData
{
    public Vector2 Position;
    public int Value;

    public DefenseData(Vector2 position, int value)
    {
        Position = position;
        Value = value;
    }
}