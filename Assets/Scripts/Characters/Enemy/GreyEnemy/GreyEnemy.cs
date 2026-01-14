using UnityEngine;

[RequireComponent(typeof(GreyEnemyAttacker))]
public class GreyEnemy : Enemy
{
    protected override void Awake()
    {
        _attacker = GetComponent<GreyEnemyAttacker>();
        base.Awake();
    }
}
