using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "StaticData/Shop")]
public class ShopStaticData : ScriptableObject
{
    [Header("InApps Settings")]
    public int CoinsValue;
    public int GemsValue;
    public MixInAppData Mix;

    [Header("Defense")]
    public int DefensePrice;
    public int DefenseValue;

    [Header("HitPoints")]
    public int HitPointsPrice;
    public int HitPointsValue;
    public int HitPointsMultiplier;
}

[Serializable]
public class MixInAppData
{
    public int Gems;
    public int Coins;
    public int Defense;
    public int HitPoints;
}