using UnityEngine;

public class EnemySounds : CharacterSounds
{
    [Header("Enemy Clips")]
    [SerializeField] private AudioClip _stepsSounds;
    [SerializeField] private AudioClip[] _attackSounds;
    [SerializeField] private AudioClip[] _hitSounds;
    [SerializeField] private AudioClip[] _deathSounds;

    private float _nextPlayStepTime;

    public void PlayStepsSound() => PlayTimedPitchSound(_stepsSounds, ref _nextPlayStepTime);
    public virtual void PlayAttackSound() => PlayRandomIndexSound(_attackSounds);
    public void PlayHitSound() => PlayRandomIndexSound(_hitSounds);
    public void PlayDeathSound() => PlayRandomIndexSound(_deathSounds);

}
