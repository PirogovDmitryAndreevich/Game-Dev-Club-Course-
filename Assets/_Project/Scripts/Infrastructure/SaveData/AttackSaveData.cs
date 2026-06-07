using System;

[Serializable]
public class AttackSaveData
{
    public PlayerAttackType Type;
    public int Level;

    public AttackSaveData(PlayerAttackType type)
    {
        Type = type;
        Level = 1;
    }
}