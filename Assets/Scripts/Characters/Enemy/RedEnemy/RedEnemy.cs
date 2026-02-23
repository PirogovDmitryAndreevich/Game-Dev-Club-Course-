using UnityEngine;

[RequireComponent(typeof(RedEnemyAttacker), typeof(RedEnemySound))]
public class RedEnemy : Enemy
{
    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        base.ApplyDamage(damageInfo, damageSource, pushDirection);
        Attacker.OnAttackEndedEvent();
    }

    protected override void CharacterAwake()
    {
        Attacker = GetComponent<RedEnemyAttacker>();
        Sound = GetComponent<RedEnemySound>();
        base.CharacterAwake();
    }
}
