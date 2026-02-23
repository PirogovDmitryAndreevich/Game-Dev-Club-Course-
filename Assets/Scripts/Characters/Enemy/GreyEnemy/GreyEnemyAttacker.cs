using UnityEngine;

public class GreyEnemyAttacker : Attacker
{
    public override AttacksType type => AttackType.Type;

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), Radius);
    }

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
}
