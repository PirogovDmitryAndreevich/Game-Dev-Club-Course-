using UnityEngine;

[RequireComponent(typeof(Mover), typeof(RedEnemySound))]
public class RedEnemyAttacker : Attacker
{
    [Header("Red enemy")]
    [SerializeField] private Mover _mover;
    [SerializeField] private RedEnemySound _enemySounds;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private float _runSpeed;
    [SerializeField] private Vector2 _attackBoxSize = new Vector2(0.5f, 0.5f);

    private Player _targetPlayer;
    private Vector2 _attackTargetPosition;

    private bool _hasHitPlayer;
    private bool _attackInProgress;

    public override AttacksType type => AttackType.Type;

    private readonly RaycastHit2D[] _hits = new RaycastHit2D[4];

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _attackBoxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), Radius);
    }

    private void FixedUpdate()
    {
        if (!_attackInProgress || !IsAttack || _targetPlayer == null)
            return;

        Vector2 currentPos = transform.position;
        Vector2 direction = (_attackTargetPosition - currentPos).normalized;

        _enemySounds.PlayAttackSound();
        
        _mover.RunAttack(_attackTargetPosition, _runSpeed);

        TryHit(direction);

        if ((currentPos - _attackTargetPosition).sqrMagnitude <= 0.0025f)
        {
            ColldownAttack = Time.time + Delay;
            AttackEnded();
        }
    }

    public override void Attack()
    {
        if (_attackInProgress || !CanAttack)
            return;

        _animator.SetEnemyAttackBool(IsAttack);

        _attackInProgress = true;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, SqrAttackDistance, TargetLayer);

        if (hit == null || !hit.TryGetComponent(out Player player))
            return;

        _targetPlayer = player;
        _attackTargetPosition = player.transform.position;

        _hasHitPlayer = false;
    }

    private void AttackEnded()
    {
        EndedAttack();
        _hasHitPlayer = false;
        _attackInProgress = false;
        _animator.SetEnemyAttackBool(IsAttack);
    }


    private void TryHit(Vector2 direction)
    {
        if (_hasHitPlayer)
            return;

        float castDistance = _runSpeed * Time.fixedDeltaTime;

        int hitCount = Physics2D.BoxCastNonAlloc(
            transform.position,
            _attackBoxSize,
            0f,
            direction,
            _hits,
            castDistance,
            TargetLayer
        );

        for (int i = 0; i < hitCount; i++)
        {
            if (_hits[i].collider != null &&
                _hits[i].collider.TryGetComponent(out Player player))
            {
                Vector2 origin = GetAttackOrigin();
                Vector2 knockbackDir =
                    (player.transform.position - transform.position).normalized;

                player.ApplyDamage(
                    AttackType,
                    _hits[i].collider.ClosestPoint(origin),
                    knockbackDir
                );

                _hasHitPlayer = true;
                break;
            }
        }        
    }
}
