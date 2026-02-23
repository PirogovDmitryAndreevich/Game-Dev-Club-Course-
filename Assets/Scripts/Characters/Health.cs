using System;
using UnityEngine;

public class Health
{
    private const int ArmorReductionInOneHit = 1;

    public bool IsShield = false;
    private int _currentHealth;
    private int _defense = 0;

    public Health(int maxHealth, int defense, bool isShield)
    {
        MaxHealth = maxHealth;
        HealthCurrent = maxHealth;
        _defense = defense;
        IsShield = isShield;
    }

    public event Action Died;
    public event Action<float, float, bool> HealthChanged;

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

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

        if (IsShield)
            ChangeDefense(-ArmorReductionInOneHit);
        else
            ChangeHealth(-damage);

        if (HealthCurrent == 0)
            Died?.Invoke();
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
        HealthChanged?.Invoke(_currentHealth, MaxHealth, IsShield);
    }

    private void ChangeDefense(int value)
    {
        Defense = Mathf.Max(0, Defense + value);
        IsShield = Defense > 0;

        HealthChanged?.Invoke(_currentHealth, MaxHealth, IsShield);
    }
}
