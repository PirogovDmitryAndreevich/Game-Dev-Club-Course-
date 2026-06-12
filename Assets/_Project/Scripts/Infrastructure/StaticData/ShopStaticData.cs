using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "StaticData/Shop")]
public class ShopStaticData : ScriptableObject
{
    [Header("Defense")]
    public int DefensePrice;
    public int DefenseValue;

    [Header("HitPoints")]
    public int HitPointsPrice;
    public int HitPointsValue;
    public int HitPointsMultiplier;
}

