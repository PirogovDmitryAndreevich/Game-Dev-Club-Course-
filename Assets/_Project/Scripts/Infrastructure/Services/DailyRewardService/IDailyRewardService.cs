public interface IDailyRewardService : IService
{
    bool CanTakeReward();

    DailyRewardType GetCurrentRewardDay();

    bool TryTakeReward(out DailyRewardStaticData reward);
}