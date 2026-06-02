using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(RedEnemySound))]
public class RammingEnemyAttacker : EnemyAttacker
{
    [Header("Ramming Enemy")]
    [SerializeField] private Mover _mover;
    [SerializeField] private RedEnemySound _enemySounds;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private DamageCollider _damageCollider;

    private Vector2 _attackTargetPosition;
    private bool _hasHitPlayer;
    private Collider2D[] _hits = new Collider2D[1];

    public override float AttackRange => Radius * Radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    private void Start()
    {
        _damageCollider.Hit += OnHit;
        _damageCollider.enabled = false;
    }

    private void OnDestroy() =>
        _damageCollider.Hit -= OnHit;

    public override void Attack()
    {
        _animator.SetEnemyAttackBool(IsAttack);

        int countHits = Physics2D.OverlapCircleNonAlloc(GetAttackOrigin(), Radius, _hits, TargetLayer);

        Collider2D hit = _hits.FirstOrDefault();

        if (hit == null || !hit.TryGetComponent(out Player player))
            return;

        _attackTargetPosition = player.transform.position;

        _damageCollider.enabled = true;
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            _mover.RunAttack(_attackTargetPosition, Data.RunSpeed);
            _enemySounds.PlayAttackSound();

            if (((Vector2)transform.position - _attackTargetPosition).sqrMagnitude <= 0.0025)
            {
                isRunning = false;
                AttackEnded();
            }

            if (_hasHitPlayer)
            {
                isRunning = false;
                AttackEnded();
            }

            yield return null;
        }
    }

    private void AttackEnded()
    {
        EndedAttack();
        _animator.SetEnemyAttackBool(IsAttack);
        _hasHitPlayer = false;
        _damageCollider.enabled = false;        
    }

    private void OnHit(Player player, Vector2 point)
    {
        if (_hasHitPlayer)
            return;

        _hasHitPlayer = true;

        Vector2 knockbackDir =
                    (player.transform.position - transform.position).normalized;

        player.ApplyDamage(Data.Damage, Data.KnockbackForce, point, knockbackDir);
    }
}
