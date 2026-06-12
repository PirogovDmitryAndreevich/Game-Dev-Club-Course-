using System;

[Serializable]
public class PlayerSaveData
{
    public int Coins;
    public int Gems;
    public int Score;
    public int Damage;
    public int Defense;
    public int Health ;
    public int HitPointUpdateCount;

    public event Action<StatsType> StatsChanged;

    public PlayerSaveData()
    {
        Coins = 0;
        Gems = 0;
        Score = 0;
        Damage = 5;
        Defense = 0;
        Health = 100;
        HitPointUpdateCount = 1;
    }

    public int GetStat(StatsType type)
    {
        int value;

        return value = type switch
        {
            StatsType.Coins => Coins,
            StatsType.Gem => Gems,
            StatsType.Score => Score,
            StatsType.Damage => Damage,
            StatsType.Defense => Defense,
            StatsType.Health => Health,
            _ => 0
        };
    }

    public void SetStat(StatsType type, int value)
    {
        switch (type)
        {
            case StatsType.Coins:
                Coins += value;
                break;

            case StatsType.Gem:
                Gems += value;
                break;

            case StatsType.Score:
                Score += value;
                break;

            case StatsType.Damage:
                Damage += value;
                break;

            case StatsType.Defense:
                Defense += value;
                break;

            case StatsType.Health:
                HitPointUpdateCount++;
                Health += value;
                break;

            default:
                return;
        }

        StatsChanged?.Invoke(type);
    }
}
