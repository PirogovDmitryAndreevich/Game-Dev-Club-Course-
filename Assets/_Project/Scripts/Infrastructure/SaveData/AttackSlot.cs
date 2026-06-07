using System;

[Serializable]
public class AttackSlot
{
    public bool IsEmpty;
    public PlayerAttackType Type;

    public event Action<PlayerAttackType> SlotSelected;
    public event Action SlotDeselected;

    public AttackSlot() => 
        IsEmpty = true;

    public void Select(PlayerAttackType type)
    {
        Type = type;
        IsEmpty = false;
        SlotSelected?.Invoke(type);
    }

    public void Deselect()
    {
        IsEmpty = true;
        Type = PlayerAttackType.Default;
        SlotDeselected?.Invoke();
    }
}
