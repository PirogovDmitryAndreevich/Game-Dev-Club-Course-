using UnityEngine;

public class DailyReward : MonoBehaviour
{
    [SerializeField] private DailyRewardButton[] _buttons;

    private IDailyRewardService _service;
    private DailyRewardButton _currentButton;

    private void OnDestroy()
    {
        if (_currentButton != null)
            _currentButton.Clicked -= OnRewardClicked;
    }

    public void Construct(IDailyRewardService service, IStaticData staticData)
    {
        _service = service;

        DailyRewardType currentDay =
            _service.GetCurrentRewardDay();

        bool canTakeReward =
            _service.CanTakeReward();

        foreach (DailyRewardButton button in _buttons)
        {
            button.Initialize(
                staticData.ForDailyReward(button.Type));

            if (button.Type < currentDay)
                button.SetClaimedView();

            if (button.Type == currentDay)
            {
                if (canTakeReward)
                {
                    button.SetActiveView();
                    _currentButton = button;
                    _currentButton.Clicked += OnRewardClicked;
                }
                else
                {
                    button.SetClaimedView();
                }
            }
        }
    }

    private void OnRewardClicked()
    {
        Debug.Log($"Clicked {_currentButton.Type}");

        if (_service.TryTakeReward(out DailyRewardStaticData reward))
        {
            Debug.Log($"Получена награда {reward.RewardCoins}");
            _currentButton.SetClaimedView();
        }
        else
        {
            Debug.Log("Награда уже получена");
        }
    }
}
