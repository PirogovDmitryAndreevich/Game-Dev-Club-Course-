using System;

public interface IHealth
{
    int HealthCurrent { get; }
    int MaxHealth { get; }

    event Action Died;
    event Action HealthChanged;

    void ApplyDamage(int damage);
}