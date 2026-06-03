using TMPro;
using UnityEngine;
using YG;
using YG.Utils.LB;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderboardYG _leaderboard;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _rankText;

    private IPersistentProgressService _progressService;

    private void Start() =>
        SetNewValueLB(StatsType.Score);

    private void OnDestroy()
    {
        _progressService.Progress.PlayerData.StatsChanged -= SetNewValueLB;
        YG2.onGetLeaderboard -= SetLeaderboardData;
    }

    public void Construct(IPersistentProgressService progressService)
    {
        _progressService = progressService;

        _progressService.Progress.PlayerData.StatsChanged += SetNewValueLB;
        YG2.onGetLeaderboard += SetLeaderboardData;
    }
    private void SetNewValueLB(StatsType type)
    {
        if (type != StatsType.Score)
            return;

        _leaderboard.SetLeaderboard(_progressService.Progress.PlayerData.GetStat(type)); 

        _leaderboard.UpdateLB();
    }
    private void SetLeaderboardData(LBData data)
    {
        _scoreText.text = data.currentPlayer.score.ToString();
        _rankText.text = data.currentPlayer.rank.ToString();
    }
}
