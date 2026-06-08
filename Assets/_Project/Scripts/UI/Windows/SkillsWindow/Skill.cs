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
    private PlayerAttacksData _progress;
    private ISaveLoadService _save;
    private DamageData _damageData;
    private DamageData _nextDamageData;

    private bool _isMaxLevel;

    public SkillLockView LockView => _lock;
    public ButtonPlaySoundOnInteract ButtonSound => _buttonSound;
    private int CurrentLevel => _progress.GetAttackSave(_attackData.Type).Level;
    private int NextLevel => CurrentLevel + 1;

    private void OnDestroy() => 
        _toggle.onValueChanged.RemoveListener(OnToggleSelect);

    public void Construct(AttackData attack, PlayerAttacksData attackSaveData, ISaveLoadService save)
    {
        _attackData = attack;
        _progress = attackSaveData;
        _save = save;

        _icon.sprite = _attackData.Icon;

        bool isSelected =
        _progress.FirstSlot.Type == _attackData.Type ||
        _progress.SecondSlot.Type == _attackData.Type;

        _toggle.SetIsOnWithoutNotify(isSelected);

        _toggle.onValueChanged.AddListener(OnToggleSelect);

        if (_progress.Contain(_attackData.Type))
            OnUnlockView(_attackData.Type);
        else
            OnLockView();

        _progress.NewAttacksOpened += OnUnlockView;
    }

    private void OnUnlockView(PlayerAttackType type)
    {
        if (type != _attackData.Type)
            return;

        _lock.Hide();
        UpdateDamageData();

        if (_isMaxLevel)
        {
            MaxView();
            return;
        }

        _damage.UpdateText(_damageData.Damage);

        _damageNext.text = "+" + _nextDamageData.Damage.ToString();
        _price.text = _nextDamageData.Price.ToString();

        _level.UpdateText(CurrentLevel);
        _levelNext.text = NextLevelText;
    }

    private void MaxView()
    {
        _priceFrame.SetActive(false);
        _price.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(false);
        _damageNext.gameObject.SetActive(false);
        _levelNext.gameObject.SetActive(false);

        _level.UpdateText(CurrentLevel);
        _damage.UpdateText(_damageData.Damage);
    }

    private void OnLockView() => 
        _lock.Show();

    private void OnToggleSelect(bool isOn)
    {
        if (isOn)
        {
            bool selected = _progress.TrySelectAttack(_attackData.Type);

            if (selected == false)
            {
                _toggle.SetIsOnWithoutNotify(false);
                return;
            }
        }
        else
        {
            _progress.DeselectAttack(_attackData.Type);
        }

            _save.SaveProgress();
    }

    private void UpdateDamageData()
    {
        _damageData = _attackData.GetDamageData(CurrentLevel);
        _nextDamageData = _attackData.GetDamageData(NextLevel);

        if (_nextDamageData == null)
            OnMaxLevel();
    }

    private void OnMaxLevel()
    {
        _isMaxLevel = true;
    }
}
