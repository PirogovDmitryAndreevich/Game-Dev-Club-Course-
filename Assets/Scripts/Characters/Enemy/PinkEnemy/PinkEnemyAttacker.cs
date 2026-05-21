using System;
using UnityEngine;

[RequireComponent(typeof(EnemySounds))]
public class PinkEnemyAttacker : Attacker
{
    [SerializeField] private AnimationEvent _attackEvent;
    [SerializeField] private EnemySounds _sound;
    [SerializeField] private float _offsetDamage = 1f;
    [SerializeField] private FXPool _fxPool;

    public override AttacksType type => AttackType.Type;

    private void Start() =>
        _attackEvent.AttackEnded += OnEndedAttackEvent;

    public override void Attack()
    {
        _sound.PlayAttackSound();

        Collider2D hit = Physics2D.OverlapCircle(transform.position, SqrAttackDistance, TargetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            var bullet = _fxPool.Get(FXType.Bullet) as Bullet;

            Vector2 point = player.transform.position;
            point.y -= _offsetDamage;
            bullet.Play(transform.position, point, AttackType, TargetLayer);
        }       
    }

    private void OnEndedAttackEvent() =>
        EndedAttack();
}
