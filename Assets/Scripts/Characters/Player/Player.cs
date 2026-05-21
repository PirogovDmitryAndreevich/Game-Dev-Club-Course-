using UnityEngine;

[RequireComponent(typeof(CollisionHandler), typeof(PlayerSounds))]
public class Player : Character
{
    [Header("Player Components")]
    [SerializeField] private PlayerFX _playerFX;
    [SerializeField] private ArrowTracking _arrowTracking;
    [SerializeField] private PlayerSounds _playerSounds;
    [SerializeField] private CameraShake _cameraShake;

    private IInputServices _input;

    public CameraShake CameraShake => _cameraShake;
    public PlayerFX FX => _playerFX;
    public ArrowTracking ArrowTracking => _arrowTracking;
    public PlayerSounds PlayerSounds => _playerSounds;
    public PlayerHealth Health { get; private set; }
    public PlayerDefense Defense { get; private set; }

    public void Construct(IPersistentProgressService progress, ISaveLoadService save, IInputServices input)
    {
        _input = input;

        Health = new PlayerHealth(progress);
        Defense = new PlayerDefense(progress);
    }

    private void FixedUpdate()
    {
        Animator.SetIsWalk(_input.Direction.x != 0
            || _input.Direction.y != 0);

        if (_input.Direction != null) //&& !Attacker.IsAttack
        {
            Mover.Move(_input.Direction);
            Fliper.LookAtTarget((Vector2)transform.position + Vector2.right * _input.Direction);

            if (_input.Direction != Vector2.zero)
                _playerSounds.PlayStepsSound();
        }

        if (_input.GetIsDash()
        && _input.Direction != null)
        {
            Mover.Stop();
            Mover.Dash(_input.Direction);
            Animator.SetPlayerDashTrigger();
            _playerSounds.PlayDash();
        }

        if (_input.GetIsAttack()) //&& Attacker.CanAttack
        {
            //Attacker.StartAttack(_camera);
            Animator.SetDefaultPlayerAttackTrigger();
            Mover.Stop();
            Mover.AttackStep();
            _playerSounds.PlayAttackSound();
        }
    }

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        base.ApplyDamage(damageInfo, damageSource, pushDirection);
        _playerSounds.PlayHitSound();
    }
}
