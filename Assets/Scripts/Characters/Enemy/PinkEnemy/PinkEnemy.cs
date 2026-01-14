using UnityEngine;

[RequireComponent(typeof(PinkEnemyAttacker))]
public class PinkEnemy : Enemy
{
    protected override void Awake()
    {
        _attacker = GetComponent<PinkEnemyAttacker>();
        base.Awake();
    }
}
