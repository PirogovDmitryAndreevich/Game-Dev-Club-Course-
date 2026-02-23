using UnityEngine;

[RequireComponent(typeof(GreyEnemyAttacker), typeof(EnemySounds))]
public class GreyEnemy : Enemy
{
    protected override void CharacterAwake()
    {
        Sound = GetComponent<EnemySounds>();
        Attacker = GetComponent<GreyEnemyAttacker>();
        base.CharacterAwake();
    }
}
