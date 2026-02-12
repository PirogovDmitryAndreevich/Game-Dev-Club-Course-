using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class FailWindow : PauseBase
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private float _appearanceDuration = 0.5f;
    [SerializeField] private float _appearanceButton = 0.5f;

    private Player _player;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
        _player.OnCharacterDied -= OnPlayerDied;
    }

    public override void Show()
    {
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(1f, _appearanceDuration)).SetEase(Ease.Flash)
            .Append(_restartButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Join(_exitButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Play();
    }

    public override void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .SetUpdate(true)
            .Append(_exitButton.transform.DOScale(0f, _appearanceButton).From(1f).SetEase(Ease.OutBounce))
            .Join(_restartButton.transform.DOScale(0f, _appearanceButton).From(1f).SetEase(Ease.OutBounce))
            .Append(_canvasGroup.DOFade(0f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => callback?.Invoke());
    }    

    internal void Initialize(Player player)
    {
        _player = player;
        _player.OnCharacterDied += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        gameObject.SetActive(true);
        Show();
    }
}
