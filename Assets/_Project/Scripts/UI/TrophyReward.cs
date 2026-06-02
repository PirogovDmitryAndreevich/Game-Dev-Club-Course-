using DG.Tweening;
using TMPro;
using UnityEngine;

public class TrophyReward : MonoBehaviour
{    
    [SerializeField] private CanvasGroup _rewardText;
    [SerializeField] private RewardIcon _rewardScore;
    [SerializeField] private RewardIcon _rewardCoins;
    [SerializeField] private RewardIcon _rewardGems;
    [SerializeField] private Transform _counterView;
    [SerializeField] private TMP_Text _trophyCounter;

    private Sequence _animation;
    private LevelData _levelData;

    public RewardIcon CoinsRewardIcon => _rewardCoins;

    public void Construct(LevelData levelData)
    {
        _levelData = levelData;

        _rewardCoins.SetText(levelData.Coins);
        _rewardGems.SetText(levelData.Gems);

        gameObject.SetActive(false);
    }

    public void Show(int trophy, int taskTrophy)
    {
        gameObject.SetActive(true);
        _trophyCounter.text = $"{0} / {taskTrophy}";

        _rewardScore.SetText(trophy * _levelData.ScoreMultiply);

        _animation = DOTween.Sequence();

        _animation
           .SetUpdate(true)
           .Append(_counterView.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Append(
               DOVirtual.Int(0, trophy, 1f, value =>
               {
                   _trophyCounter.text = $"{value} / {taskTrophy}";
               }))
           .Append(_rewardText.DOFade(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Join(_rewardScore.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Join(_rewardCoins.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Join(_rewardGems.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))           
           .Play()
           .OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        _animation.Kill();
    }
}
