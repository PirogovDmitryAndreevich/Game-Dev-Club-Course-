using System;

[Serializable]
public class EnemyStaticData 
{
    public EnemyTypeId TypeId;
    public int MaxHealth;
    public int Damage;
    public float Radius;
    public float Offset;
    public float KnockbackForce;
    public float MaxSqrDistance = 13.7f;
    public float TryFindTime = 1f;
    public float WaitTime = 2.0f;
    public bool IsKnockback;
    public bool IsCrit;

    public Enemy EnemyPrefab;
}
