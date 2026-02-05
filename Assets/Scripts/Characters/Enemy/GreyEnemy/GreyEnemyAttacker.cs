using UnityEngine;

public class GreyEnemyAttacker : Attacker
{
    public override AttacksType type => _attack.Type;

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
        {
            Vector2 pushDirection = _fliper.IsTernRight
                                        ? Vector2.right
                                            : Vector2.left;

            player.ApplyDamage(_attack, hit.ClosestPoint(origin), pushDirection);
        }
    }
}
