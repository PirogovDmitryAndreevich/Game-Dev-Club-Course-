using UnityEngine;

[RequireComponent(typeof(RedEnemyAttacker))]
public class RedEnemy : Enemy
{
    protected override void Awake()
    {
        _attacker = GetComponent<RedEnemyAttacker>();
        base.Awake();
    }
}
