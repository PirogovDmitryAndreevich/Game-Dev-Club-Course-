using System;

[Serializable]
public class Attack
{
    public PlayerAttackType Type;
    public int Damage;
    public float AttackOffset;
    public float AttackRadius;
    public float KnockbackForce;
    public bool IsCreat;
}