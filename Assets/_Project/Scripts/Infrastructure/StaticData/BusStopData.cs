using System;
using UnityEngine;

[Serializable]
public class BusStopData
{
    public Vector2 Position;
    public BusData BusData;

    public BusStopData(Vector2 position, BusData busData)
    {
        Position = position;
        BusData = busData;
    }
}
