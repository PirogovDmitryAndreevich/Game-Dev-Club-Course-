using UnityEngine;

public class PlayerSounds : CharacterSounds
{
    [Header("Player Clips")]
    [SerializeField] private AudioClip[] _stepsSounds;
    [SerializeField] private AudioClip[] _attackSounds;
    [SerializeField] private AudioClip[] _hitSound;

    private float _nextPlayStepTime;

    public void PlayStepsSound() => PlayTimedRandomIndexSound(_stepsSounds, ref _nextPlayStepTime);
    public void PlayAttackSound() => PlayRandomIndexSound(_attackSounds);
    public void PlayHitSound() => PlayRandomIndexSound(_hitSound);

}
