using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private DailyRewardType _type;
    [SerializeField] private Transform _focus;
    [SerializeField] private Transform _dim;

    public event Action Clicked;

    public DailyRewardType Type => _type;

    private void OnDestroy() =>
        _button.onClick.RemoveListener(OnClicked);

    private void Awake() =>
        _button.onClick.AddListener(OnClicked);

    public void Initialize(DailyRewardStaticData dailyRewardStaticData)
    {
        _button.interactable = false;
        _rewardText.text = dailyRewardStaticData.RewardCoins.ToString();

        _focus.gameObject.SetActive(false);
        _dim.gameObject.SetActive(false);
    }

    public void SetActiveView()
    {
        _button.interactable = true;
        _focus.gameObject.SetActive(true);
        _dim.gameObject.SetActive(false);
    }

    private void OnClicked() => 
        Clicked?.Invoke();

    public void SetClaimedView()
    {
        _button.interactable = false;
        _focus.gameObject.SetActive(false);
        _dim.gameObject.SetActive(true);
    }
}
