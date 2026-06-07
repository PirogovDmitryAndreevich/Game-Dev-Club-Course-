using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWidow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _panelView;
    [SerializeField] private CanvasGroup _anticlicker;
    [SerializeField] private Button _closeButton;

    [Header("AudioSettings")]
    [SerializeField] private AudioSettings _audioSettings;
    [SerializeField] private AudioClip _hideShowSound;

    [Header("Animation setting")]
    [SerializeField] private float _ACDuration;
    [SerializeField] private float _panelScaleDuration;

    private RectTransform _panelRectTransform;
    private Sequence Animation;
    AudioHandler AudioHandler;

    public AudioSettings AudioSlider => _audioSettings;
    private bool IsAnimating =>
        Animation != null && Animation.active;

    private void Start() =>
        gameObject.SetActive(false);

    private void OnEnable() => 
        _closeButton.onClick.AddListener(OnClickCloseButton);

    private void OnDisable() => 
        _closeButton.onClick.RemoveListener(OnClickCloseButton);

    public void Construct(AudioHandler audio)
    {
        AudioHandler = audio;
        _panelRectTransform = _panelView.GetComponent<RectTransform>();
    }

    public void Hide()
    {
        AudioHandler.PlaySound(_hideShowSound);

        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_panelRectTransform.DOScale(0f, _panelScaleDuration).From(1))
            .Join(_panelView.DOFade(0f, _panelScaleDuration))
            .Append(_anticlicker.DOFade(0f, _ACDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => gameObject.SetActive(false));
    }

    public void Show()
    {
        AudioHandler.PlaySound(_hideShowSound);

        gameObject.SetActive(true);

        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_anticlicker.DOFade(1f, _ACDuration)).SetEase(Ease.Flash)
            .Append(_panelView.DOFade(1f, _panelScaleDuration))
            .Join(_panelRectTransform.DOScale(1f, _panelScaleDuration).From(0).SetEase(Ease.OutBounce))
            .Play();
    }

    private void KillCurrentAnimationIfActive()
    {
        if (IsAnimating)
            Animation.Kill();
    }

    private void OnClickCloseButton() => 
        Hide();
}
