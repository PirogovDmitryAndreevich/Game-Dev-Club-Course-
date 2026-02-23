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

    private void Awake()
    {
        _fillImage = _slider.fillRect.GetComponent<Image>();
        _shield?.SetActive(false);
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    public void Initialize(Health health)
    {
        _health = health;
        _health.HealthChanged += OnHealthChanged;
        OnHealthChanged(_health.HealthCurrent, _health.MaxHealth, _health.IsShield);
    }

    private void OnHealthChanged(float currentValue, float maxValue, bool isShield)
    {
        _shield.SetActive(isShield);

        _fillImage.color = isShield ? _shieldColor : _healthColor;
        _slider.value = currentValue / maxValue;
        _textValue.text = isShield ? $"{_health.Defense}" : $"{currentValue}/{maxValue}";

    }
}
