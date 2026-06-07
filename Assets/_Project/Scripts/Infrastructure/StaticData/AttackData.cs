using System;
using UnityEngine;

[Serializable]
public class AttackData
{
    public PlayerAttackType Type;
    public float AttackOffset;
    public float AttackRadius;
    public float KnockbackForce;
    public bool IsCreat;

    public Sprite Icon;

    public DamageData[] Damage;

    public DamageData GetDamageData(int level)
    {
        int index = level - 1;

        if (index < Damage.Length)
            return Damage[index];
        else
            return null;
    }
}
