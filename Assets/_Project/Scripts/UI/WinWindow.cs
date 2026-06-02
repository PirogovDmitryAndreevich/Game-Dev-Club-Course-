using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class WinWindow : PauseBase
{
    [SerializeField] private Button _advButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Image _nextButtonImage;
    [SerializeField] private TrophyReward _trophyCounter;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _appearanceDuration = 0.5f;
    [SerializeField] private float _appearanceButton = 0.5f;

    private SceneID _nextSceneID;
    private IPersistentProgressService _progress;
    private ISaveLoadService _save;
    private LevelData _levelData;
    private int _trophies;
    private int _allTrophies;

    public TrophyReward TrophyCounter => _trophyCounter;
    protected override AudioHandler AudioHandler { get; set; }
    protected override GameStateMachine GameStateMachine { get; set; }
    protected override SceneID CurrentScene { get; set; }

    public void Construct(SceneID current, SceneID next, GameStateMachine stateMachine, AudioHandler handler,
        IPersistentProgressService progressService, ISaveLoadService save, LevelData levelData)
    {
        CurrentScene = current;
        GameStateMachine = stateMachine;
        AudioHandler = handler;
        _nextSceneID = next;
        _progress = progressService;
        _save = save;
        _levelData = levelData;
    }

    public void Initialize(int trophyReached, int trophies)
    {
        _trophies = trophyReached;
        _allTrophies = trophies;
    }

    public override void Show()
    {
        KillCurrentAnimationIfActive();

        GiveOutRewards();
        _trophyCounter.Show(_trophies, _allTrophies);

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_canvasGroup.DOFade(1f, _appearanceDuration)).SetEase(Ease.Flash)
            .Append(_restartButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Join(_exitButton.transform.DOScale(1f, _appearanceButton).From(0f).SetEase(Ease.OutBounce))
            .Play();

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
            .Append(_advButton.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
            .Play()
            .OnComplete(() => callback?.Invoke());
    }

    protected override void Enable()
    {
        base.Enable();
        bool isNextLevel = _nextSceneID != SceneID.Non;
        _nextButton.interactable = isNextLevel;
        _nextButtonImage.color = isNextLevel ? _nextButtonImage.color : Color.gray;

        _advButton.onClick.AddListener(OnRewardButton);
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
        _nextButton.onClick.AddListener(NextLevel);
    }

    protected override void Disable()
    {
        base.Disable();
        _advButton.onClick.RemoveListener(OnRewardButton);
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
        _nextButton.onClick.RemoveListener(NextLevel);
    }

    private void NextLevel() =>
        LoadScene(_nextSceneID);

    private void GiveOutRewards()
    {
        _progress.Progress.PlayerData.SetStat(StatsType.Score, _trophies * _levelData.ScoreMultiply);
        _progress.Progress.PlayerData.SetStat(StatsType.Gem, _levelData.Gems);
        _progress.Progress.PlayerData.SetStat(StatsType.Coins, _levelData.Coins);

        _save.SaveProgress();
    }

    private void OnRewardButton()
    {
        //reward
        _trophyCounter.CoinsRewardIcon.SetText(_levelData.Coins * _levelData.Coins);
    }
}
