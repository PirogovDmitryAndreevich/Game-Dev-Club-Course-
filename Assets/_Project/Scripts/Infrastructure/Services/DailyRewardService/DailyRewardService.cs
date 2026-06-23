using System;
using YG;

public class DailyRewardService : IDailyRewardService
{
    private readonly IStaticData _staticData;
    private readonly IPersistentProgressService _progress;
    private readonly ISaveLoadService _save;

    public DailyRewardService(IStaticData staticData, IPersistentProgressService progress,
        ISaveLoadService save)
    {
        _staticData = staticData;
        _progress = progress;
        _save = save;
    }

    public bool CanTakeReward() =>
    GetDaysFromLastReward(YG2.ServerTime()) >= 1;

    public DailyRewardType GetCurrentRewardDay()
    {
        int days = GetDaysFromLastReward(YG2.ServerTime());

        if (_progress.Progress.DailyReward.LastDailyRewardTime == 0)
            return DailyRewardType.Day_1;

        if (days > 1)
        {
            ResetRewardDay();
            return DailyRewardType.Day_1;
        }

        if (days == 0)
            return _progress.Progress.DailyReward.LastClaimedDay;

        return _progress.Progress.DailyReward.NextRewardDay;
    }

    public bool TryTakeReward(out DailyRewardStaticData reward)
    {
        reward = null;

        long serverTime = YG2.ServerTime();
        int days = GetDaysFromLastReward(serverTime);

        if (days < 1)
            return false;

        if (days > 1)
            ResetRewardDay();

        DailyRewardType day =
            _progress.Progress.DailyReward.NextRewardDay;

        reward = _staticData.ForDailyReward(day);

        GiveReward(reward);

        _progress.Progress.DailyReward.LastClaimedDay = day;
        _progress.Progress.DailyReward.NextRewardDay = day.Next();
        _progress.Progress.DailyReward.LastDailyRewardTime = serverTime;

        _save.SaveProgress();

        return true;
    }

    private void GiveReward(DailyRewardStaticData reward)
    {
        _progress.Progress.PlayerData.SetStat(
            StatsType.Coins,
            reward.RewardCoins);

        _progress.Progress.PlayerData.SetStat(
            StatsType.Gem,
            reward.RewardGems);
    }

    private int GetDaysFromLastReward(long serverTime)
    {
        long lastTime =
            _progress.Progress.DailyReward.LastDailyRewardTime;

        if (lastTime == 0)
            return 1;

        DateTime lastDate =
            DateTimeOffset
                .FromUnixTimeMilliseconds(lastTime)
                .UtcDateTime
                .Date;

        DateTime currentDate =
            DateTimeOffset
                .FromUnixTimeMilliseconds(serverTime)
                .UtcDateTime
                .Date;

        return (currentDate - lastDate).Days;
    }

    private void ResetRewardDay()
    {
        _progress.Progress.DailyReward.NextRewardDay =
            DailyRewardType.Day_1;

        _progress.Progress.DailyReward.LastClaimedDay = default;
    }
}
