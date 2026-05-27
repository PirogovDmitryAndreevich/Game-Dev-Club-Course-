using System;
using UnityEngine;

[Serializable]
public class KeyData
{
    public Vector2 Position;

    public KeyData(Vector2 position)
    {
        Position = position;
    }
}