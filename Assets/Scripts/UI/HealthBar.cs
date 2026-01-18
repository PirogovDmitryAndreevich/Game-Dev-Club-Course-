using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    private Health _health;

    private void OnDestroy()
    {
        _health.OnHealthChanged -= OnHealthChanged;
    }

    public void Initialize(Health health)
    {
        _health = health;
        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float currentValue, float maxValue)
    {
        _fill.fillAmount = currentValue / maxValue;
    }
}
