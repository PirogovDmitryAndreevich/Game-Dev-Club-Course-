using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWidow : PauseBase
{
    [SerializeField] private CanvasGroup _panelView;
    [SerializeField] private CanvasGroup _anticlicker;
    [SerializeField] private Button _closeButton;

    [Header("Animation setting")]
    [SerializeField] private float _ACDuration;
    [SerializeField] private float _panelScaleDuration;

    private RectTransform _panelRectTransform;

    private void Awake()
    {
        _panelView.alpha = 0f;
        _anticlicker.alpha = 0f;
        _panelRectTransform = _panelView.GetComponent<RectTransform>();
    }

    public override void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_panelRectTransform.DOScale(0f, _panelScaleDuration).From(1))
            .Join(_panelView.DOFade(0f, _panelScaleDuration))
            .Append(_anticlicker.DOFade(0f, _ACDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => callback?.Invoke());
    }

    public override void Show()
    {
        _panelView.gameObject.SetActive(true);
        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_anticlicker.DOFade(1f, _ACDuration)).SetEase(Ease.Flash)
            .Append(_panelView.DOFade(1f, _panelScaleDuration))
            .Join(_panelRectTransform.DOScale(1f, _panelScaleDuration).From(0).SetEase(Ease.OutBounce))
            .Play();
    }

    protected override void Enable()
    {
        _closeButton.onClick.AddListener(Continue);
    }

    protected override void Disable()
    {
        _closeButton.onClick.RemoveListener(Continue);
    }
}
