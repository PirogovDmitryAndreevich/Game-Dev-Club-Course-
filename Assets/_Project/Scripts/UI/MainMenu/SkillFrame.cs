using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkillFrame : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _skillIcon;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _lockView;

    private IPersistentProgressService _progress;
    private IStaticData _staticData;
    private SkillsWindow _skillWindow;
    private AttackSaveData _attackProgress;
    private PlayerAttackType _currentType;

    private void OnDestroy() => 
        _button.onClick.RemoveListener(ShowSkillWindow);

    public void Construct(IPersistentProgressService progressService, IStaticData staticData, SkillsWindow skillsWindow)
    {
        _progress = progressService;
        _staticData = staticData;
        _skillWindow = skillsWindow;

        _button.onClick.AddListener(ShowSkillWindow);
    }

    public void SelectAttack(PlayerAttackType type)
    {
        if (_attackProgress != null)
            DeselectAttack();

        _currentType = type;

        AttackData attack = _staticData.ForPlayerAttack(_currentType);
        _attackProgress = _progress.Progress.PlayerAttacksData.GetAttackSave(_currentType);

        _skillIcon.gameObject.SetActive(true);
        _skillIcon.sprite = attack.Icon;

        _levelText.gameObject.SetActive(true);
        _levelText.text = _attackProgress.Level.ToString();

        SubscribeChanges();
    }

    public void DeselectAttack()
    {
        _skillIcon.gameObject.SetActive(false);
        _levelText.gameObject.SetActive(false);
        _attackProgress = null;
        _currentType = PlayerAttackType.Default;

        UnsubscribeChanges();
    }

    private void SubscribeChanges() => 
        _attackProgress.LevelUpped += UpdateText;

    private void UnsubscribeChanges()
    {
        if (_attackProgress != null)
            _attackProgress.LevelUpped -= UpdateText;
    }

    private void UpdateText(PlayerAttackType type) => 
        _levelText.text = _attackProgress.Level.ToString();

    private void ShowSkillWindow()
    {
        YG2.InterstitialAdvShow();
        //_skillWindow.gameObject.SetActive(true);
        _skillWindow.Show();
    }
}