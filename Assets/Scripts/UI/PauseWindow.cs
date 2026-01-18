using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : PauseBase
{
    [SerializeField] private CanvasGroup _panelView;
    [SerializeField] private CanvasGroup _anticlicker;

    [Header("Animation setting")]
    [SerializeField] private float _ACDuration;
    [SerializeField] private float _panelScaleDuration;

    [Header("Buttons")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    private RectTransform _panelRectTransform;

    private void Awake()
    {
        _panelView.alpha = 0f;
        _anticlicker.alpha = 0f;
        _panelRectTransform = _panelView.GetComponent<RectTransform>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _restartButton.onClick.AddListener(Restart);
        _continueButton.onClick.AddListener(Continue);
        _exitButton.onClick.AddListener(Exit);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _restartButton.onClick.RemoveListener(Restart);
        _continueButton.onClick.RemoveListener(Continue);
        _exitButton.onClick.RemoveListener(Exit);
    }

    public override void Show()
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .Append(_anticlicker.DOFade(1f, _ACDuration)).SetEase(Ease.Flash).Play()
            .Append(_panelView.DOFade(1f, _panelScaleDuration)).Play()
            .Join(_panelRectTransform.DOScale(1f, _panelScaleDuration).From(0).SetEase(Ease.OutBounce)).Play();

    }

    public override void Hide(Action callback)
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .Append(_panelRectTransform.DOScale(0f, _panelScaleDuration).From(1)).Play()
            .Join(_panelView.DOFade(0f, _panelScaleDuration)).Play()
            .Append(_anticlicker.DOFade(0f, _ACDuration)).Play()
            .OnComplete(() => callback?.Invoke());
    }
}
