using System;
using UnityEngine;

public class Health
{
    public Health(int maxHealth)
    {
        MaxHealth = maxHealth;
        HealthCurrent = maxHealth;
    }

    public event Action OnDied;

    public event Action<float, float> OnHealthChanged;

    public int MaxHealth { get; private set; }

    public int HealthCurrent
    {
        get => _currentValue;
        private set
        {
            if (_currentValue != value)
            { 
                _currentValue = value;
                OnHealthChanged?.Invoke(_currentValue, MaxHealth);
            }
        }
    }

    private int _currentValue;

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

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

    private void ChangeHealth(int value)
    {
        HealthCurrent = Mathf.Clamp(HealthCurrent + value, 0, MaxHealth);
    }
}
