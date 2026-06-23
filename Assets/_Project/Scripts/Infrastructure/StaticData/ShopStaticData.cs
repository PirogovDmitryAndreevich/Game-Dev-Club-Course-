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

    [Header("DailyReward")]
    public DailyRewardStaticData[] DailyRewardsData;
}
