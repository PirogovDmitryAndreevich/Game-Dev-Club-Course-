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
        Vector2 origin = GetAttackOrigin();

        Collider2D hit = Physics2D.OverlapCircle(origin, _radius, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))        
            player.ApplyDamage(_damage, hit.ClosestPoint(origin));        
    }
}
