using UnityEngine;

public class RedEnemySound : EnemySounds
{
    [Header("RedEnemy clips")]
    [SerializeField] private AudioClip _runAttackSound;

    private float _nextPlayTimeAttack;

    public override void PlayAttackSound()
    {
        PlayTimedPitchSound(_runAttackSound, ref _nextPlayTimeAttack);
    }
}
