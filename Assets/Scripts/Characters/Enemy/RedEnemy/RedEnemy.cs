using UnityEngine;

[RequireComponent(typeof(RedEnemyAttacker), typeof(RedEnemySound))]
public class RedEnemy : Enemy
{
    protected override void Awake()
    {
        _attacker = GetComponent<RedEnemyAttacker>();
        _sound = GetComponent<RedEnemySound>();
        base.Awake();
    }

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        base.ApplyDamage(damageInfo, damageSource, pushDirection);
        _attacker.OnAttackEndedEvent();
    }
}
