using UnityEngine;

[RequireComponent(typeof(PinkEnemyAttacker), typeof(EnemySounds))]
public class PinkEnemy : Enemy
{
    protected override void CharacterAwake()
    {
        Attacker = GetComponent<PinkEnemyAttacker>();
        Sound = GetComponent<EnemySounds>();
        base.CharacterAwake();
    }
}
