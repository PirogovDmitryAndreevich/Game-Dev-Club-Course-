using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    private const string NextLevelText = "+1";

    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _icon;
    [SerializeField] private SkillTextAnimation _damage;
    [SerializeField] private TMP_Text _damageNext;
    [SerializeField] private SkillTextAnimation _level;
    [SerializeField] private TMP_Text _levelNext;
    [SerializeField] private GameObject _priceFrame;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buyButton;
    [SerializeField] private ButtonPlaySoundOnInteract _buttonSound;
    [SerializeField] private SkillLockView _lock;

    private AttackData _attackData;
    private IPersistentProgressService _progress;
    private PlayerAttacksData _playerAttackSaveData;
    private AttackSaveData _attackSaveData;
    private ISaveLoadService _save;
    private DamageData _damageData;
    private DamageData _nextDamageData;

    private bool _isMaxLevel;
    private bool _wasUnlockView;
    private bool _wasLockView;

    public SkillLockView LockView => _lock;
    public ButtonPlaySoundOnInteract ButtonSound => _buttonSound;
    private int CurrentLevel => _playerAttackSaveData.GetAttackSave(_attackData.Type).Level;
    private int NextLevel => CurrentLevel + 1;

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleSelect);

        if (_wasUnlockView)
        {
            _attackSaveData.LevelUpped -= OnLevelUpped;
            _buyButton.onClick.RemoveListener(OnBuyButton);
        }

        if (_wasLockView)
        {
            _playerAttackSaveData.NewAttacksOpened -= OnUnlockAttack;
            _lock.BuyButtonPressed -= OnLockViewPressed;
        }
    }

    public void Construct(AttackData attack, IPersistentProgressService progressService, ISaveLoadService save)
    {
        _attackData = attack;
        _progress = progressService;
        _playerAttackSaveData = progressService.Progress.PlayerAttacksData;
        
        _save = save;
        _icon.sprite = _attackData.Icon;

        SetToggleView();
        _toggle.onValueChanged.AddListener(OnToggleSelect);

        if (_playerAttackSaveData.Contain(_attackData.Type))
            StartUnlockView();
        else
            StartLockView();
    }

    private void StartUnlockView()
    {
        _lock.Hide();
        _attackSaveData = _playerAttackSaveData.GetAttackSave(_attackData.Type);

        UpdateDamageData();
        UpdateCurrentDataText();

        if (_isMaxLevel)
        {
            MaxView();
            return;
        }

        _wasUnlockView = true;

        _attackSaveData.LevelUpped += OnLevelUpped;
        _buyButton.onClick.AddListener(OnBuyButton);

        UpdateNextDataText();
    }

    private void StartLockView()
    {
        _lock.Show();

        _wasLockView = true;
        _playerAttackSaveData.NewAttacksOpened += OnUnlockAttack;
        _lock.BuyButtonPressed += OnLockViewPressed;
    }

    private void OnUnlockAttack(PlayerAttackType type)
    {
        if (type != _attackData.Type)
            return;

        StartUnlockView();
    }

    private void OnLevelUpped(PlayerAttackType type)
    {
        if (type != _attackData.Type)
            return;

        UpdateDamageData();
        UpdateCurrentDataText();

        if (_isMaxLevel)
        {
            MaxView();
            return;
        }

        UpdateNextDataText();
    }

    private void MaxView()
    {
        _priceFrame.SetActive(false);
        _price.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(false);
        _damageNext.gameObject.SetActive(false);
        _levelNext.gameObject.SetActive(false);

        UpdateCurrentDataText();
    }

    private void OnBuyButton()
    {
        if (_nextDamageData == null)
            return;

        if (_progress.Progress.PlayerData.Coins < _nextDamageData.Price)
            return;

        _progress.Progress.PlayerData.SetStat(StatsType.Coins, -_nextDamageData.Price);
        _attackSaveData.LevelUp(_attackData);
        _save.SaveProgress();
    }

    private void OnLockViewPressed()
    {
        if (_progress.Progress.PlayerData.Coins < _attackData.Damage[0].Price)
            return;

        _playerAttackSaveData.OpenNewAttacks(_attackData.Type);
        _progress.Progress.PlayerData.SetStat(StatsType.Coins, -_attackData.Damage[0].Price);
        _save.SaveProgress();
    }

    private void SetToggleView()
    {
        bool isSelected =
        _playerAttackSaveData.FirstSlot.Type == _attackData.Type ||
        _playerAttackSaveData.SecondSlot.Type == _attackData.Type;

        _toggle.SetIsOnWithoutNotify(isSelected);
    }

    private void OnToggleSelect(bool isOn)
    {
        if (isOn)
        {
            bool selected = _playerAttackSaveData.TrySelectAttack(_attackData.Type);

            if (selected == false)
            {
                _toggle.SetIsOnWithoutNotify(false);
                return;
            }
        }
        else
        {
            _playerAttackSaveData.DeselectAttack(_attackData.Type);
        }

        _save.SaveProgress();
    }

    private void UpdateDamageData()
    {
        _damageData = _attackData.GetDamageData(CurrentLevel);
        _nextDamageData = _attackData.GetDamageData(NextLevel);

        if (_nextDamageData == null)
            _isMaxLevel = true;
    }

    private void UpdateCurrentDataText()
    {
        _damage.UpdateText(_damageData.Damage);
        _level.UpdateText(CurrentLevel);
    }

    private void UpdateNextDataText()
    {
        _damageNext.text = "+" + _nextDamageData.Damage.ToString();
        _price.text = _nextDamageData.Price.ToString();

        _levelNext.text = NextLevelText;
    }
}
