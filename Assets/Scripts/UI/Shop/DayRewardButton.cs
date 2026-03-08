using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DayRewardButton : MonoBehaviour
{
    [SerializeField] private int _dayIndex;
    [SerializeField] private RewardType _rewardType;
    [SerializeField] private int _rewardAmount;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private Transform _focus;
    [SerializeField] private Transform _dim;

    private Button _button;

    public Button RewardButton => _button;
    public RewardType Type => _rewardType;
    public int DayIndex => _dayIndex;
    public int RewardAmount => _rewardAmount;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _amountText.text = _rewardAmount.ToString();
        _focus.gameObject.SetActive(false);
        _dim.gameObject.SetActive(false);
    }

    public void SetFocus(bool isActivate)
    {
        _focus.gameObject.SetActive(isActivate);
    }

    public void SetDim(bool isDim)
    {
        _dim.gameObject.SetActive(isDim);
    }

}
