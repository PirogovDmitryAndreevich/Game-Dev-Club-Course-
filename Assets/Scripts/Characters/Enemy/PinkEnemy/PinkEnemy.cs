using UnityEngine;

[RequireComponent(typeof(PinkEnemyAttacker), typeof(EnemySounds))]
public class PinkEnemy : Enemy
{
    protected override void Awake()
    {
        _attacker = GetComponent<PinkEnemyAttacker>();
        _sound = GetComponent<EnemySounds>();
        base.Awake();
    }
}
