using UnityEngine;

public class SkillsContainer : MonoBehaviour
{
    [SerializeField] private SkillFrame _firstSkill;
    [SerializeField] private SkillFrame _secondSkill;

    private IPersistentProgressService _progress;

    private AttackSlot _firstSlot;
    private AttackSlot _secondSlot;

    public SkillFrame FirstSkill => _firstSkill;
    public SkillFrame SecondSkill => _secondSkill;

    private void OnDestroy()
    {
        _firstSlot.SlotSelected -= SelectFirstSkill;
        _firstSlot.SlotDeselected -= DeselectFirstSkill;

        _secondSlot.SlotSelected -= SelectSecondSkill;
        _secondSlot.SlotDeselected -= DeselectSecondSkill;
    }

    private void Start()
    {
        if (!_firstSlot.IsEmpty)
            SelectFirstSkill(_firstSlot.Type);
        else
            DeselectFirstSkill();

        if (!_secondSlot.IsEmpty)
            SelectSecondSkill(_secondSlot.Type);
        else
            DeselectSecondSkill();
    }

    public void Construct(IPersistentProgressService progressService)
    {
        _progress = progressService;

        _firstSlot = _progress.Progress.PlayerAttacksData.FirstSlot;
        _secondSlot = _progress.Progress.PlayerAttacksData.SecondSlot;

        _firstSlot.SlotSelected += SelectFirstSkill;
        _firstSlot.SlotDeselected += DeselectFirstSkill;

        _secondSlot.SlotSelected += SelectSecondSkill;
        _secondSlot.SlotDeselected += DeselectSecondSkill;
    }

    private void SelectFirstSkill(PlayerAttackType type) =>
        _firstSkill.SelectAttack(type);

    private void DeselectFirstSkill() =>
        _firstSkill.DeselectAttack();

    private void SelectSecondSkill(PlayerAttackType type) =>
        _secondSkill.SelectAttack(type);

    private void DeselectSecondSkill() =>
        _secondSkill.DeselectAttack();
}
