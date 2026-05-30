using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrophyCounter : MonoBehaviour
{
    [SerializeField] private Button _scoreButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Transform _counterView;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _trophyCounter;

    private Sequence _animation;
    private AudioHandler _audio;

    private void OnDestroy()
    {
        _scoreButton.onClick.RemoveListener(OnScoreButton);
        _rewardButton.onClick.RemoveListener(OnRewardButton);
    }

    public void Construct(AudioHandler audio)
    {
        _audio = audio;

        _scoreButton.onClick.AddListener(OnScoreButton);
        _rewardButton.onClick.AddListener(OnRewardButton);

        gameObject.SetActive(false);
    }

    public void Show(int trophy, int taskTrophy)
    {
        gameObject.SetActive(true);
        _trophyCounter.text = $"{0} / {taskTrophy}";

        _animation = DOTween.Sequence();

        _animation
           .SetUpdate(true)
           .Append(_counterView.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Append(
               DOVirtual.Int(0, trophy, 1f, value =>
               {
                   _trophyCounter.text = $"{value} / {taskTrophy}";
               }))
           .Append(_scoreButton.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Join(_rewardButton.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
           .Play()
           .OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        _animation.Kill();

        _scoreButton.enabled = true;
        _rewardButton.enabled = true;
    }

    private void OnScoreButton()
    {
        gameObject.SetActive(false);
    }

    private void OnRewardButton()
    {
        gameObject.SetActive(false);
    }
}
