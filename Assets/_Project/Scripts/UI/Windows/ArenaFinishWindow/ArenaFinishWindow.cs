using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ArenaFinishWindow : PauseBase
{
    private const int ScoreCoefficient = 1000;
    private const int CoinsCoefficient = 1000;
    [SerializeField] private Button _advButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private RewardForArenaView _rewardInfo;

    private IPersistentProgressService _progress;
    private ISaveLoadService _save;

    private float _time;
    private int _score;
    private int _coins;
    private int _gems;

    protected override AudioHandler AudioHandler { get; set; }
    protected override GameStateMachine GameStateMachine { get; set; }
    protected override SceneID CurrentScene { get; set; }


    public void Construct(GameStateMachine stateMachine, AudioHandler handler,
        IPersistentProgressService progressService, ISaveLoadService save)
    {
        GameStateMachine = stateMachine;
        AudioHandler = handler;
        CurrentScene = SceneID.Arena;

        _progress = progressService;
        _save = save;

        _advButton.interactable = true;
    }

    public void Initialize(float time)
    {
        _time = time;
    }

    public override void Hide(Action callback)
    {
        KillCurrentAnimationIfActive();

        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);

        YG2.InterstitialAdvShow();

        KillCurrentAnimationIfActive();

        AudioHandler.PlayMainMenuMusic();

        int minutes = Mathf.FloorToInt(_time / 60f);

        _score = minutes * ScoreCoefficient;
        _coins = minutes * CoinsCoefficient;
        _gems = minutes;

        GiveOutRewards();
        
        _rewardInfo.Show(_time, _coins, _gems, _score);

        Animation = DOTween.Sequence();

        Animation
            .SetUpdate(true)
            .Append(_restartButton.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
            .Join(_exitButton.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
            .Play();
    }

    protected override void Enable()
    {
        base.Enable();

        _advButton.onClick.AddListener(OnRewardButton);
        _restartButton.onClick.AddListener(Restart);
        _exitButton.onClick.AddListener(Exit);
    }

    protected override void Disable()
    {
        base.Disable();

        _advButton.onClick.RemoveListener(OnRewardButton);
        _restartButton.onClick.RemoveListener(Restart);
        _exitButton.onClick.RemoveListener(Exit);
    }

    private void GiveOutRewards()
    {
        var record = _progress.Progress.LevelsProgress.ArenaSave.RecordTime;

        if (_time > record)
            OnNewRecord();

        _progress.Progress.PlayerData.SetStat(StatsType.Score, _score);
        _progress.Progress.PlayerData.SetStat(StatsType.Gem, _gems);
        _progress.Progress.PlayerData.SetStat(StatsType.Coins, _coins);

        _save.SaveProgress();
    }

    private void OnNewRecord()
    {
        _progress.Progress.LevelsProgress.ArenaSave.UpdateRecord(_time);
    }

    private void OnRewardButton() =>
        YG2.RewardedAdvShow("Gold", () => GiveReward());

    private void GiveReward()
    {
        _advButton.interactable = false;
        _rewardInfo.CoinsRewardIcon.SetText(_coins * 2);
        _progress.Progress.PlayerData.SetStat(StatsType.Coins, _coins);

        _save.SaveProgress();
    }
}
