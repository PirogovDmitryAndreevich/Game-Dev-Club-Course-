using System;

[System.Serializable]
public class PlayerSaveData
{
    public int _coins = 0;
    public int _gems = 0;
    public int _score = 0;
    public int _damage = 5;
    public int _defense = 0;
    public int _health = 100;

    public Action<StatsType> StatsChanged;

    public int Coins
    { 
        get => _coins;
        set => SetStat(ref _coins, value, StatsType.Coins);
    }
    public int Gems
    {
        get => _gems;
        set => SetStat(ref _gems, value, StatsType.Gem);
    }
    public int Score
    {
        get => _score;
        set => SetStat(ref _score, value, StatsType.Score);
    }
    public int Damage
    {
        get => _damage;
        set => SetStat(ref _damage, value, StatsType.Damage);
    }
    public int Defense
    {
        get => _defense;
        set => SetStat(ref _defense, value, StatsType.Defense);
    }
    public int Health
    {
        get => _health;
        set => SetStat(ref _health, value, StatsType.Health);
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

    private void SetStat(ref int field, int value, StatsType type)
    {
        if (field == value)
            return;

        field = value;
        StatsChanged?.Invoke(type);        
    }    
}
