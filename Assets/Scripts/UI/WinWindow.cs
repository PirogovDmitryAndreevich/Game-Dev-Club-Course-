using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class WinWindow : PauseBase
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Image _nextButtonImage;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _appearanceDuration = 0.5f;
    [SerializeField] private float _appearanceButton = 0.5f;

    private SceneID _nextSceneID;

    protected override AudioHandler AudioHandler { get; set; }
    protected override GameStateMachine GameStateMachine { get; set; }
    protected override SceneID CurrentScene { get; set; }

    public void Construct(SceneID current, SceneID next, GameStateMachine stateMachine, AudioHandler handler)
    {
        CurrentScene = current;
        GameStateMachine = stateMachine;
        AudioHandler = handler;
        _nextSceneID = next;
    }

    public override void Show()
    {
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

    protected override void Enable()
    {
        base.Enable();
        bool isNextLevel = _nextSceneID != SceneID.Non;
        _nextButton.interactable = isNextLevel;
        _nextButtonImage.color = isNextLevel ? _nextButtonImage.color : Color.gray;

        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
        _nextButton.onClick.AddListener(NextLevel);
    }

    protected override void Disable()
    {
        base.Disable();
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
        _nextButton.onClick.RemoveListener(NextLevel);
    }

    private void NextLevel() => 
        LoadScene(_nextSceneID);
}
