using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private GameObject _shield;
    [SerializeField] private Color _healthColor;
    [SerializeField] private Color _shieldColor;

    private Health _health;
    private Image _fillImage;
    private bool _isShield;

    private void Awake()
    {
        _fillImage = _slider.fillRect.GetComponent<Image>();
        _shield.SetActive(false);
    }

    private void OnDestroy()
    {
        _health.OnHealthChanged -= OnHealthChanged;
    }

    public void Initialize(Health health)
    {
        _health = health;
        _health.OnHealthChanged += OnHealthChanged;
        OnHealthChanged(_health.HealthCurrent, _health.MaxHealth, false);
    }

    private void OnHealthChanged(float currentValue, float maxValue, bool isShield)
    {
        _slider.value = currentValue / maxValue;
        _textValue.text = $"{currentValue}/{maxValue}";
                
        if (_isShield != isShield)
        {
            _isShield = isShield;
            _fillImage.color = _isShield ? _shieldColor : _healthColor;
            _shield.SetActive(_isShield);
        }
    }
}
