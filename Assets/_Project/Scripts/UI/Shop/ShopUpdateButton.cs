using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpdateButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private float _scaleMultiplayer;

    private Sequence _animation;
    private int _price;
    private int _value;

    public event Action ButtonPressed;

    private void OnEnable()
    {
        _animation?.Play();
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnClick);

    public void Initialize(int newPrice, int newValue)
    {
        _price = newPrice;
        _value = newValue;

        _priceText.text = _price.ToString();
        _valueText.text = _value.ToString();
    }

    public void OnClick() =>
        ButtonPressed?.Invoke();

    public void UpdateView(int newPrice, int newValue)
    {
        _animation = DOTween.Sequence();

        _price = newPrice;
        _value = newValue;

        Vector2 originalPriceScale = _priceText.transform.localScale;
        Vector2 originalValueScale = _valueText.transform.localScale;

        _animation
            .SetUpdate(true)
            .Append(
            _priceText.transform.DOScale(_priceText.transform.localScale * _scaleMultiplayer, 0.3f)
            .SetEase(Ease.OutBounce))
            .Join(
            _valueText.transform.DOScale(_valueText.transform.localScale * _scaleMultiplayer, 0.3f)
            .SetEase(Ease.OutBounce))
            .AppendCallback(() =>
            {
                _priceText.text = _price.ToString();
            })
            .Append(_priceText.transform.DOScale(originalPriceScale, 0.3f).SetEase(Ease.OutBounce))
            .Join(
            _valueText.transform.DOScale(originalValueScale, 0.3f)
            .SetEase(Ease.OutBounce))
            .Play();
    }
}