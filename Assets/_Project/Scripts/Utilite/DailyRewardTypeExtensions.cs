public static class DailyRewardTypeExtensions
{
    public static DailyRewardType First =>
        DailyRewardType.Day_1;

    public static DailyRewardType Last =>
        DailyRewardType.Day_7;

    public static DailyRewardType Next(this DailyRewardType current)
    {
        return current switch
        {
            DailyRewardType.Day_1 => DailyRewardType.Day_2,
            DailyRewardType.Day_2 => DailyRewardType.Day_3,
            DailyRewardType.Day_3 => DailyRewardType.Day_4,
            DailyRewardType.Day_4 => DailyRewardType.Day_5,
            DailyRewardType.Day_5 => DailyRewardType.Day_6,
            DailyRewardType.Day_6 => DailyRewardType.Day_7,
            _ => First
        };
    }
}
