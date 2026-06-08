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

    public void LevelUp()
    {
        Level++;
        LevelUpped?.Invoke(Type);
    }
}