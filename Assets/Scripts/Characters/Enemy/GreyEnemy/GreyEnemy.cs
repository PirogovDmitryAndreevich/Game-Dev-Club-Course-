using UnityEngine;

[RequireComponent(typeof(GreyEnemyAttacker), typeof(EnemySounds))]
public class GreyEnemy : Enemy
{
    protected override void Awake()
    {
        _sound = GetComponent<EnemySounds>();
        _attacker = GetComponent<GreyEnemyAttacker>();
        base.Awake();
    }
}
