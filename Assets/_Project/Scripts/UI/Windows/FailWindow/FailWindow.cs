using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(CanvasGroup))]
public class FailWindow : PauseBase
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _appearanceDuration = 0.5f;
    [SerializeField] private float _appearanceButton = 0.5f;

    private Player _player;

    protected override AudioHandler AudioHandler { get; set; }
    protected override GameStateMachine GameStateMachine { get; set; }
    protected override SceneID CurrentScene { get; set; }

    private void OnDestroy()
    {
        _player.Health.Died -= OnPlayerDied;
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
    }

    public void Construct(SceneID current, GameStateMachine stateMachine, AudioHandler audio, Player player)
    {
        CurrentScene = current;
        GameStateMachine = stateMachine;
        AudioHandler = audio;
        _player = player;

        _player.Health.Died += OnPlayerDied;
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    public override void Show()
    {
        YG2.InterstitialAdvShow();

        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(1f, _appearanceDuration)).SetEase(Ease.Flash)
            .Append(_restartButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Join(_exitButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Play();
    }

    public override void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_exitButton.transform.DOScale(0f, _appearanceButton).From(1f).SetEase(Ease.OutBounce))
            .Join(_restartButton.transform.DOScale(0f, _appearanceButton).From(1f).SetEase(Ease.OutBounce))
            .Append(_canvasGroup.DOFade(0f, _appearanceDuration)).SetEase(Ease.Flash)
            .Play()
            .OnComplete(() => callback?.Invoke());
    }

    private void OnPlayerDied()
    {
        gameObject.SetActive(true);
        Show();
    }
}
