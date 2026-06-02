using System.Linq;
using UnityEngine;

public class MeleeEnemyAttacker : EnemyAttacker
{
    [SerializeField] private AnimationEvent _attackEvent;
    [SerializeField] private CharacterAnimator _animator;

    private Collider2D[] _hits = new Collider2D[1];

    public override float AttackRange => Offset * Offset;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), AttackRange);
    }

    private void Start()
    {
        _attackEvent.AttackEnded += OnEndedAttackEvent;
        _attackEvent.DealDamage += OnDealDamage;
    }

    public override void Attack() => 
        _animator.SetEnemyAttackTrigger();

    private void OnDealDamage()
    {
        Vector2 origin = GetAttackOrigin();

        int countHits = Physics2D.OverlapCircleNonAlloc(GetAttackOrigin(), Radius, _hits, TargetLayer);

        Collider2D hit = _hits.FirstOrDefault();

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            Vector2 pushDirection = Fliper.IsTernRight
                                        ? Vector2.right
                                            : Vector2.left;

            player.ApplyDamage(Data.Damage, Data.KnockbackForce, hit.ClosestPoint(origin), pushDirection);
        }
    }

    private void OnEndedAttackEvent() => 
        EndedAttack();
}
