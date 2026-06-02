using TMPro;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private TMP_Text _gems;

    private IPersistentProgressService _progress;

    private void OnDestroy() => 
        _progress.Progress.PlayerData.StatsChanged -= UpdateStats;

    public void Construct(IPersistentProgressService persistentProgress)
    {
        _progress = persistentProgress;

        _progress.Progress.PlayerData.StatsChanged += UpdateStats;

        UpdateCoins();
        UpdateGems();
    }

    private void UpdateStats(StatsType type)
    {
        switch (type)
        {
            case StatsType.Coins:
                UpdateCoins();
                break;
            case StatsType.Gem:
                UpdateGems();
                break;
        }
    }

    private void UpdateCoins() => 
        _coins.text = _progress.Progress.PlayerData.Coins.ToString();

    private void UpdateGems() => 
        _gems.text = _progress.Progress.PlayerData.Coins.ToString();
}