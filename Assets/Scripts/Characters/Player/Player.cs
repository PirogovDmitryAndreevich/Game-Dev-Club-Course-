using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler), typeof(InputReader), typeof(PlayerAttacker))]
[RequireComponent(typeof(CameraShake), typeof(PlayerSounds))]
public class Player : Character
{
    [SerializeField] private HealthBar _healthBar;

    private InputReader _inputReader;
    private CollisionHandler _collisionHandler;
    private CameraShake _camera;
    private PlayerSounds _sound;

    private IInteractable _interactable;

    protected override void Awake()
    {
        _maxHealth = SaveData.PlayerData.Health;
        _attacker = GetComponent<PlayerAttacker>();
        _inputReader = GetComponent<InputReader>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _camera = GetComponent<CameraShake>();
        _sound = GetComponent<PlayerSounds>();

        _collisionHandler.InteractStarted += OnInteractStarted;
        base.Awake();
        _healthBar.Initialize(_health);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _collisionHandler.InteractStarted -= OnInteractStarted;
    }

    protected override void FixedUpdate()
    {
        _animator.SetIsWalk(_inputReader.Direction.x != 0 
                            || _inputReader.Direction.y != 0);

        if (_inputReader.Direction != null && !_attacker.IsAttack)
        {
            _mover.Move(_inputReader.Direction);
            _fliper.LookAtTarget((Vector2)transform.position + Vector2.right * _inputReader.Direction);

            if (_inputReader.Direction != Vector2.zero)
                _sound.PlayStepsSound();
        }

        if (_inputReader.GetIsDash()
        && _inputReader.Direction != null)
        {
            _mover.Stop();
            _mover.Dash(_inputReader.Direction);
            _animator.SetPlayerDashTrigger();
        }

        if (_inputReader.GetIsAttack() && _attacker.CanAttack)
        {
            _attacker.StartAttack(_camera);
            _animator.SetDefaultPlayerAttackTrigger();
            _mover.Stop();
            _mover.AttackStep();
            _sound.PlayAttackSound();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnInteractStarted(IInteractable interactableObject)
    {
        _interactable = interactableObject;
    }

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        base.ApplyDamage(damageInfo, damageSource, pushDirection);
        _sound.PlayHitSound();
    }

    public void Heal(int value)
    {
        _health.Heal(value);
    }
}
