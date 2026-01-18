using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class RedEnemyAttacker : EnemyAttacker
{
    [Header("Red enemy")]
    [SerializeField] private float _runSpeed;

    private Mover _mover;
    private bool _isAttacking;

    protected override void Start()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isAttacking)
        {
            Debug.Log("IsAttacking true");

            if (collision.TryGetComponent(out Player player))
            {
                Debug.Log(collision.name);

                Vector2 direction = (player.transform.position - transform.position).normalized;

                player.ApplyDamageWithKnockback(_damage, direction);
                _isAttacking = false;
            }
        }
    }

    public override void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, SqrAttackDistance, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
            StartCoroutine(Attacking(player.transform.position));
    }

    public override void OnAttackEndedEvent()
    {
        Debug.Log("RedEnemy: OnAttackEndedEvent");
        _isAttacking = false;
        base.OnAttackEndedEvent();
    }

    private IEnumerator Attacking(Vector2 target)
    {
        _isAttacking = true;

        while (IsAttack)
        {
            _mover.RunAttack(target, _runSpeed);
            yield return null;
        }
    }
}
