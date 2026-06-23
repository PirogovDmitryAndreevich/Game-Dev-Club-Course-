using System;

[Serializable]
public class DailyRewardData
{
    public long LastDailyRewardTime;
    public DailyRewardType NextRewardDay = DailyRewardType.Day_1;
    public DailyRewardType LastClaimedDay;
}