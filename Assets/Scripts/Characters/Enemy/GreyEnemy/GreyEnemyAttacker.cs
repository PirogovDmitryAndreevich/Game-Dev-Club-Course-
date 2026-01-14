using UnityEngine;

public class GreyEnemyAttacker : EnemyAttacker
{
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), _radius);
    }

    public override void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(GetAttackOrigin(), _radius, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))        
            player.ApplyDamage(_damage);        
    }
}
