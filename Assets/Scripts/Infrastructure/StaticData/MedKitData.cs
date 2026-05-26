using System;
using UnityEngine;

[Serializable]
public class MedKitData
{
    public Vector2 Position;
    public int Value;

    public MedKitData(Vector2 position, int value)
    {
        Position = position;
        Value = value;
    }
}
