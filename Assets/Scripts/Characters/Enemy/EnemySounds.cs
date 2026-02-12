using UnityEngine;

public class EnemySounds : CharacterSounds
{
    [Header("Enemy Clips")]
    [SerializeField] private AudioClip _stepsSounds;
    [SerializeField] private AudioClip[] _attackSounds;
    [SerializeField] private AudioClip[] _hitSounds;
    [SerializeField] private AudioClip[] _deathSounds;

    private float _nextPlayStepTime;

    public void PlayStepsSound()
    {
        if (!AudioManager.IsLoaded) return;

        PlayTimedPitchSound(_stepsSounds, ref _nextPlayStepTime);
    }

    public virtual void PlayAttackSound()
    {
        if (!AudioManager.IsLoaded) return;

        PlayRandomIndexSound(_attackSounds);
    }

    public void PlayHitSound()
    {
        if (!AudioManager.IsLoaded) return;

        PlayRandomIndexSound(_hitSounds);
    }

    public void PlayDeathSound()
    {
        if (!AudioManager.IsLoaded) return;

        PlayRandomIndexSound(_deathSounds);
    }
}
