using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _shield;
    [SerializeField] private Color _healthColor;
    [SerializeField] private Color _shieldColor;

    private PlayerHealth _health;
    private PlayerDefense _defense;

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
        
        UpdateView();
    }

    private void UpdateView()
    {
        _shield.SetActive(_defense.IsShield);

        _fillImage.color = _defense.IsShield ? _shieldColor : _healthColor;
        _slider.value = (float)_health.HealthCurrent / _health.MaxHealth;

        _textValue.text = _defense.IsShield 
            ? $"{_defense.Defense}" 
            : $"{_health.HealthCurrent}/{_health.MaxHealth}";
    }
}
