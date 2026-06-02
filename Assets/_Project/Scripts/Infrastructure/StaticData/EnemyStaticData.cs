using System;
using UnityEngine;

[Serializable]
public class EnemyStaticData 
{
    public EnemyTypeId TypeId;
    public int MaxHealth;
    public float MaxSqrDistance = 13.7f;
    public float TryFindTime = 1f;
    public float WaitTime = 2.0f;
    public int Reward = 200;
    public int GemPercent = 30;

    [Header("Mover Data")]
    public float RunSpeed;

    [Header("Attack Data")]
    public int Damage;
    public float AttackRadius;
    public float AttackOffset;
    public float ColdawnAttack;
    public float KnockbackForce;

    public Enemy EnemyPrefab;

}
