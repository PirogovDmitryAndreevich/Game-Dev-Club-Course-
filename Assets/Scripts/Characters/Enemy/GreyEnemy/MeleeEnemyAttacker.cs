using UnityEngine;

public class MeleeEnemyAttacker : EnemyAttacker
{
    [SerializeField] private AnimationEvent _attackEvent;
    [SerializeField] private CharacterAnimator _animator;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), Radius);
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

        Collider2D hit = Physics2D.OverlapCircle(origin, Radius, TargetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            Vector2 pushDirection = Fliper.IsTernRight
                                        ? Vector2.right
                                            : Vector2.left;

            player.ApplyDamage(Damage, KnockbackForce, hit.ClosestPoint(origin), pushDirection);
        }
    }

    private void OnEndedAttackEvent() => 
        EndedAttack();
}
