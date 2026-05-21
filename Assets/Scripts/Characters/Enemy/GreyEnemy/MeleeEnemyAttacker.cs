using System;
using UnityEngine;

public class MeleeEnemyAttacker : Attacker
{
    [SerializeField] private AnimationEvent _attackEvent;

    public override AttacksType type => AttackType.Type;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), Radius);
    }

    private void Start() =>
        _attackEvent.AttackEnded += OnEndedAttackEvent;

    public override void Attack()
    {
        Vector2 origin = GetAttackOrigin();

        Collider2D hit = Physics2D.OverlapCircle(origin, Radius, TargetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            Vector2 pushDirection = Fliper.IsTernRight
                                        ? Vector2.right
                                            : Vector2.left;

            player.ApplyDamage(AttackType, hit.ClosestPoint(origin), pushDirection);
        }
    }

    private void OnEndedAttackEvent() =>
        EndedAttack();
}
