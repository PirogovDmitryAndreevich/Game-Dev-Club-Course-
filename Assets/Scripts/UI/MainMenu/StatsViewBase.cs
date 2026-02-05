using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class StatsViewBase : MonoBehaviour
{
    protected abstract StatsType _type { get; }

    [SerializeField] protected TMP_Text _valueText;
    [SerializeField] protected Button _addButton;

    private PlayerSaveData _playerData;

    private void OnEnable()
    {
        if (SaveData.IsLoaded)
            OnEnableUpdate();
        else
            SaveData.OnLoaded += OnEnableUpdate;
    }

    private void OnDisable()
    {
        SaveData.OnLoaded -= OnEnableUpdate;

        _playerData.StatsChanged -= UpdateCoins;

        if (_addButton != null)
            _addButton.onClick.RemoveListener(OnClickButton);
    }

    protected virtual void OnEnableUpdate()
    {
        _playerData = SaveData.PlayerData;
        _playerData.StatsChanged += UpdateCoins;

        if (_addButton != null)
            _addButton.onClick.AddListener(OnClickButton);

        UpdateCoins(_type);
    }

    protected virtual void UpdateCoins(StatsType type)
    {
        if (_type == type)
            _valueText.text = _playerData.GetStat(_type).ToString();
    }

    protected virtual void OnClickButton()
    {
        Debug.Log($"Pressed add {GetType()} button");
    }


}
