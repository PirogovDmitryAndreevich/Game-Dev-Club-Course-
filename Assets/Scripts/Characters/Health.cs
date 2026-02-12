using System;
using UnityEngine;

public class Health
{
    private const int ArmorReductionInOneHit = 1;

    public Health(int maxHealth, int defense, bool isShield)
    {
        MaxHealth = maxHealth;
        HealthCurrent = maxHealth;
        _defense = defense;
        IsShield = isShield;
    }

    public event Action OnDied;

    public event Action<float, float, bool> OnHealthChanged;

    public int MaxHealth { get; private set; }
    public int HealthCurrent
    {
        get => _currentHealth;
        private set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;                
            }
        }
    }

    public bool IsShield = false;
    public int Defense
    {
        get => _defense;
        set
        {
            if (_defense != value)
            {
                _defense = value;
            }

        }
    }

    private int _currentHealth;
    private int _defense = 0;

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

        if (IsShield)
            ChangeDefense(-ArmorReductionInOneHit);
        else
            ChangeHealth(-damage);

        if (HealthCurrent == 0)
            OnDied?.Invoke();
    }

    public void Heal(int value)
    {
        if (value < 0)
            return;

        ChangeHealth(value);
    }

    public void AddDefense(int value)
    {
        if (value < 0)
            return;

        ChangeDefense(value);
    }

    private void ChangeHealth(int value)
    {
        HealthCurrent = Mathf.Clamp(HealthCurrent + value, 0, MaxHealth);
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth, IsShield);
    }

    private void ChangeDefense(int value)
    {
        Defense = Mathf.Max(0, Defense + value);
        IsShield = Defense > 0;

        OnHealthChanged?.Invoke(_currentHealth, MaxHealth, IsShield);
    }
}
