public abstract class EnemyAttacker : Attacker
{
    protected EnemyStaticData Data;

    public abstract float AttackRange {get;}
    public override float CooldownTime => Data.ColdawnAttack;
    public override float Offset => Data.AttackOffset;
    public override float Radius => Data.AttackRadius;

    public void Construct(EnemyStaticData data) => 
        Data = data;
}
