using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private Button _addButton;
    [SerializeField] private StatsType _type;
    private ShopWindow _shopWindow;
    private PlayerSaveData _playerData;

    private void OnDestroy()
    {
        _playerData.StatsChanged -= UpdateText;

        if (_addButton != null)
            _addButton.onClick.RemoveListener(OnClickButton);
    }

    public void Construct(IPersistentProgressService progressService, ShopWindow shopWindow)
    {
        _shopWindow = shopWindow;
        _playerData = progressService.Progress.PlayerData;

        _playerData.StatsChanged += UpdateText;

        if (_addButton != null)
            _addButton.onClick.AddListener(OnClickButton);

        UpdateText(_type);
    }

    private void UpdateText(StatsType type)
    {
        if (_type == type)
            _valueText.text = _playerData.GetStat(_type).ToString();
    }

    private void OnClickButton()
    {
        _shopWindow.Show();
    }
}
