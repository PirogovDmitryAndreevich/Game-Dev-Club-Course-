using System;
using UnityEngine;

[Serializable]
public class LockData
{
    public Vector2 Position;
    public Color Color;
    public KeyData Key;

    public LockData(Vector2 position, Color color, KeyData key)
    {
        Position = position;
        Color = color;
        Key = key;
    }
}
