using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class StatsViewBase : MonoBehaviour
{
    [SerializeField] protected TMP_Text ValueText;
    [SerializeField] protected Button AddButton;

    private PlayerSaveData _playerData;

    protected abstract StatsType Type { get; }

    private void OnEnable()
    {
        if (SaveData.IsLoaded)
            OnEnableUpdate();
        else
            SaveData.Loaded += OnEnableUpdate;
    }

    private void OnDisable()
    {
        SaveData.Loaded -= OnEnableUpdate;

        _playerData.StatsChanged -= UpdateCoins;

        if (AddButton != null)
            AddButton.onClick.RemoveListener(OnClickButton);
    }

    protected virtual void OnEnableUpdate()
    {
        _playerData = SaveData.PlayerData;
        _playerData.StatsChanged += UpdateCoins;

        if (AddButton != null)
            AddButton.onClick.AddListener(OnClickButton);

        UpdateCoins(Type);
    }

    protected virtual void UpdateCoins(StatsType type)
    {
        if (Type == type)
            ValueText.text = _playerData.GetStat(Type).ToString();
    }

    protected virtual void OnClickButton()
    {
        Debug.Log($"Pressed add {GetType()} button");
    }
}
