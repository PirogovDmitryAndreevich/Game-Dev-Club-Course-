using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PlayerAttacksData
{
    public List<AttackSaveData> OpenedAttacks;

    public AttackSlot FirstSlot;
    public AttackSlot SecondSlot;

    public event Action<PlayerAttackType> NewAttacksOpened;

    public PlayerAttacksData()
    {
        OpenedAttacks = new List<AttackSaveData>();
        FirstSlot = new AttackSlot();
        SecondSlot = new AttackSlot();
    }

    public void OpenNewAttacks(PlayerAttackType type)
    {
        if (Contain(type))
            return;

        AttackSaveData newData = new AttackSaveData(type);

        OpenedAttacks.Add(newData);
        NewAttacksOpened?.Invoke(newData.Type);
    }

    public bool Contain(PlayerAttackType type) =>
        OpenedAttacks.Any(x => x.Type == type);

    public AttackSaveData GetAttackSave(PlayerAttackType type) =>
        OpenedAttacks.FirstOrDefault(x => x.Type == type);

    public bool TrySelectAttack(PlayerAttackType attack)
    {
        if (FirstSlot.Type == attack || SecondSlot.Type == attack)
            return false;

        if (FirstSlot.IsEmpty)
        {
            FirstSlot.Select(attack);

            return true;
        }

        if (SecondSlot.IsEmpty)
        {
            SecondSlot.Select(attack);

            return true;
        }

        return false;
    }

    public bool DeselectAttack(PlayerAttackType attack)
    {
        if (!FirstSlot.IsEmpty && FirstSlot.Type == attack)
        {
            FirstSlot.Deselect();

            if (!SecondSlot.IsEmpty)
            {
                PlayerAttackType movedAttack = SecondSlot.Type;

                SecondSlot.Deselect();
                FirstSlot.Select(movedAttack);

                return true;
            }

            return true;
        }

        if (!SecondSlot.IsEmpty && SecondSlot.Type == attack)
        {
            SecondSlot.Deselect();

            return true;
        }

        return false;
    }
}
