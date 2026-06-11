using System;

[Serializable]
public class AttackSaveData
{
    public PlayerAttackType Type;
    public int Level;

    public event Action<PlayerAttackType> LevelUpped;

    public AttackSaveData(PlayerAttackType type)
    {
        Type = type;
        Level = 1;
    }

    public void LevelUp(AttackData attack)
    {
        DamageData damageData = attack.GetDamageData(Level + 1);

        if (damageData != null)
        {
            Level++;
            LevelUpped?.Invoke(Type);
        }
    }
}