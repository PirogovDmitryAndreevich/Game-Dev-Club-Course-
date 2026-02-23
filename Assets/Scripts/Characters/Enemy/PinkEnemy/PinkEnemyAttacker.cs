using System;
using UnityEngine;

[RequireComponent(typeof(EnemySounds))]
public class PinkEnemyAttacker : Attacker
{
    [SerializeField] private float _offsetDamage = 1f;
    [SerializeField] private FXPool _fxPool;

    private EnemySounds _sound;

    public override AttacksType type => AttackType.Type;

    public override void Attack()
    {
        _sound.PlayAttackSound();

        Collider2D hit = Physics2D.OverlapCircle(transform.position, SqrAttackDistance, TargetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            var bullet = _fxPool.Get(FXType.Bullet) as Bullet;

            Vector2 point = player.transform.position;
            point.y -= _offsetDamage;

            bullet.ReturnToPool += ReturnFXToPool;
            bullet.Play(transform.position, point, AttackType, TargetLayer);
        }       
    }

    protected override void AttackAwake()
    {
        _sound = GetComponent<EnemySounds>();
        base.AttackAwake();
    }

    private void ReturnFXToPool(FXBase fx)
    {
        fx.ReturnToPool -= ReturnFXToPool;
        _fxPool.Return(fx);
    }
}
