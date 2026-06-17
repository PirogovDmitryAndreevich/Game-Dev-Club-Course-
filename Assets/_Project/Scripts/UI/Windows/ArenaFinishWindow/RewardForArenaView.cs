using DG.Tweening;
using TMPro;
using UnityEngine;

public class RewardForArenaView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _rewardText;
    [SerializeField] private RewardIcon _rewardScore;
    [SerializeField] private RewardIcon _rewardCoins;
    [SerializeField] private RewardIcon _rewardGems;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private Transform _counterView;

    private Sequence _animation;

    public RewardIcon CoinsRewardIcon => _rewardCoins;

    public void Show(float time, int coins, int gems, int score)
    {
        UpdateTimer(time);
        _rewardScore.SetText(score);
        _rewardCoins.SetText(coins);
        _rewardGems.SetText(gems);

        _animation = DOTween.Sequence();

        _animation
           .SetUpdate(true)
           .Append(_counterView.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Append(_rewardText.DOFade(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Join(_rewardScore.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Join(_rewardCoins.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Join(_rewardGems.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Play()
           .OnComplete(() => _animation.Kill());
    }

    public void UpdateTimer(float time)
    {
        int totalSeconds = Mathf.Max(0, Mathf.CeilToInt(time));

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        string timerText = string.Format("{0:00}:{1:00}", minutes, seconds);

        _time.text = timerText;
    }
}