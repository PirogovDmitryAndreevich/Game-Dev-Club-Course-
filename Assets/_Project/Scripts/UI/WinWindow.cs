using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class WinWindow : PauseBase
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Image _nextButtonImage;
    [SerializeField] private TrophyCounter _trophyCounter;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _appearanceDuration = 0.5f;
    [SerializeField] private float _appearanceButton = 0.5f;

    private SceneID _nextSceneID;
    private IPersistentProgressService _progress;
    private ISaveLoadService _save;
    private int _trophies;
    private int _allTrophies;

    public TrophyCounter TrophyCounter => _trophyCounter;
    protected override AudioHandler AudioHandler { get; set; }
    protected override GameStateMachine GameStateMachine { get; set; }
    protected override SceneID CurrentScene { get; set; }

    public void Construct(SceneID current, SceneID next, GameStateMachine stateMachine, AudioHandler handler,
        IPersistentProgressService progressService, ISaveLoadService save)
    {
        CurrentScene = current;
        GameStateMachine = stateMachine;
        AudioHandler = handler;
        _nextSceneID = next;
        _progress = progressService;
        _save = save;
    }

    public void Initialize(int trophyReached, int trophies)
    {
        _trophies = trophyReached;
        _allTrophies = trophies;
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
            .Play()
            .OnComplete(ShowTrophyCounter);

        if (_nextSceneID != SceneID.Non)
        {
            _progress.Progress.LevelsProgress.OpenNewLevel(_nextSceneID);
            _save.SaveProgress();
        }
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

    private void ShowTrophyCounter()
    {
        _trophyCounter.Show(_trophies, _allTrophies);
    }
}
