using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : PauseBase
{
    [SerializeField] private CanvasGroup _panelView;
    [SerializeField] private CanvasGroup _anticlicker;

    [Header("Animation setting")]
    [SerializeField] private float _ACDuration;
    [SerializeField] private float _panelScaleDuration;
    [SerializeField] private AudioClip _showSound;
    [SerializeField] private AudioClip _hideSound;

    [Header("Buttons")]
    [SerializeField] private TMP_Text _textTitle;
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
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .SetUpdate(true)
            .Append(_anticlicker.DOFade(1f, _ACDuration)).SetEase(Ease.Flash)
            .Append(_panelView.DOFade(1f, _panelScaleDuration))
            .Join(_panelRectTransform.DOScale(1f, _panelScaleDuration).From(0).SetEase(Ease.OutBounce))
            .Append(_restartButton.transform.DOScale(1.1f, 0.35f).From(0).SetEase(Ease.OutBack))
            .Join(_exitButton.transform.DOScale(1.1f, 0.35f).From(0).SetEase(Ease.OutBack))
            .Append(_restartButton.transform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad))
            .Join(_exitButton.transform.DOScale(1f, 0.15f).SetEase(Ease.OutQuad))
            .Play();

        if(_showSound != null)
            AudioManager.Instance.PlaySound(_showSound);
    }

    public override void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .SetUpdate(true)
            .Append(_panelRectTransform.DOScale(0f, _panelScaleDuration).From(1))
            .Join(_panelView.DOFade(0f, _panelScaleDuration))
            .Append(_anticlicker.DOFade(0f, _ACDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => callback?.Invoke());

        if (_hideSound != null)
            AudioManager.Instance.PlaySound(_hideSound);
    }
}
