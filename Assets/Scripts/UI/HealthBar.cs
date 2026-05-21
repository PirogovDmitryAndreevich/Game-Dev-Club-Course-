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

    private PlayerHealth _health;
    private PlayerDefense _defense;
    private Image _fillImage;

    private void Awake() => 
        _fillImage = _slider.fillRect.GetComponent<Image>();

    private void OnDestroy()
    {
        _health.HealthChanged -= UpdateView;
        _defense.DefenseChanged -= UpdateView;
    }

    public void Construct(PlayerHealth health, PlayerDefense defense)
    {
        _health = health;
        _defense = defense;

        _health.HealthChanged += UpdateView;
        _defense.DefenseChanged += UpdateView;

        _shield?.SetActive(_defense.IsShield);
    }

    private void UpdateView()
    {
        _shield.SetActive(_defense.IsShield);

        _fillImage.color = _defense.IsShield ? _shieldColor : _healthColor;
        _slider.value = _health.HealthCurrent / _health.MaxHealth;

        _textValue.text = _defense.IsShield 
            ? $"{_defense.Defense}" 
            : $"{_health.HealthCurrent}/{_health.MaxHealth}";
    }
}
