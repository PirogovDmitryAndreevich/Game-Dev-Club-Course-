using System;
using UnityEngine;

public class DaysRewards : MonoBehaviour
{
    [SerializeField] private DayRewardButton[] _rewardButtons;

    private int _currentDay;

    private void Awake()
    {
        //_currentDay = ;
        SetCurrentButton();
    }

    private void OnDisable()
    {
        _rewardButtons[_currentDay].RewardButton.onClick.RemoveListener(OnClickButton);
    }

    private void SetCurrentButton()
    {
        _rewardButtons[_currentDay].RewardButton.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        RewardType currentType = _rewardButtons[_currentDay].Type;

    }
}
