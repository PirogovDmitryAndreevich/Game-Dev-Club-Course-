using UnityEngine;

[RequireComponent(typeof(PinkAttackRealization))]
public class PinkEnemyAttacker : EnemyAttacker
{
    private PinkAttackRealization _realization;

    protected override void Start()
    {
        _realization = GetComponent<PinkAttackRealization>();
    }

    public override void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, SqrAttackDistance, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))        
            _realization.StartAttack(player.transform.position, _damage, _targetLayer);        
    }
}
