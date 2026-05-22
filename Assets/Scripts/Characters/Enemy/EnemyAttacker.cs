public abstract class EnemyAttacker : Attacker
{
    private EnemyStaticData _data;

    public int Damage => _data.Damage;

    public float KnockbackForce => _data.KnockbackForce;

    public override float CooldownTime => _data.ColdawnAttack;

    public override float Offset => _data.AttackOffset;

    public override float Radius => _data.AttackRadius;

    public void Construct(EnemyStaticData data) => 
        _data = data;
}
