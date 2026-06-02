using System;
using UnityEngine;

public class EnemyHealth : IHealth
{
    public EnemyHealth(EnemyStaticData data)
    {
        MaxHealth = data.MaxHealth;
        HealthCurrent = MaxHealth;
    }

    public int HealthCurrent {  get; private set; }

    public int MaxHealth { get; private set; }

    public event Action Died;
    public event Action HealthChanged;

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

        ChangeHealth(-damage);

        if (HealthCurrent == 0)
            Died?.Invoke();
    }

    private void ChangeHealth(int value)
    {
        HealthCurrent = Mathf.Clamp(HealthCurrent + value, 0, MaxHealth);
        HealthChanged?.Invoke();
    }
}