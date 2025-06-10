using UnityEngine;

public class Health
{
    public Health(int maxHealth)
    {
        MaxHealth = maxHealth;
        HealthCurrent = maxHealth;
    }

    public int MaxHealth { get; private set; }

    public int HealthCurrent { get; private set; }


    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

        ChangeHealth(-damage);
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
