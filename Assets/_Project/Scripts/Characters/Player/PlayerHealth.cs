using System;
using UnityEngine;

public class PlayerHealth : IHealth
{
    private IPersistentProgressService _progress;

    public PlayerHealth(IPersistentProgressService progressService)
    {
        _progress = progressService;

        MaxHealth = _progress.Progress.PlayerData.GetStat(StatsType.Health);

        HealthCurrent = MaxHealth;
    }

    public event Action Died;
    public event Action HealthChanged;
    public event Action Healed;

    public int MaxHealth { get; private set; }
    public int HealthCurrent { get; private set; }

    public void ApplyDamage(int damage)
    {
        if (damage <= 0)
            return;

        ChangeHealth(-damage);

        if (HealthCurrent == 0)
            Died?.Invoke();
    }

    public void Heal(int value)
    {
        if (value < 0)
            return;

        Healed?.Invoke();

        ChangeHealth(value);
    }

    private void ChangeHealth(int value)
    {
        HealthCurrent = Mathf.Clamp(HealthCurrent + value, 0, MaxHealth);
        HealthChanged?.Invoke();
    }
}
