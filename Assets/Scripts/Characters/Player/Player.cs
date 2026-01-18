using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputReader), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler), typeof(Fliper), typeof(PlayerAttacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 20;
    [SerializeField] private PlayerAttackAnimationEvent _attackEvents;
    [SerializeField] private HitFlash _hitFlash;
    [SerializeField] private PunchAnimation _punch;
    [SerializeField] private HealthBar _healthBar;

    private Mover _mover;
    private InputReader _inputReader;
    private PlayerAnimator _animator;
    private CollisionHandler _collisionHandler;
    private Fliper _fliper;
    private PlayerAttacker _attacker;
    private Health _health;

    private IInteractable _interactable;

    public event Action OnPlayerDied;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _healthBar.Initialize(_health);
        _attacker = GetComponent<PlayerAttacker>();
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _fliper = GetComponent<Fliper>();
    }

    private void OnEnable()
    {
        _health.OnDied += OnDied;
        _collisionHandler.InteractStarted += OnInteractStarted;
        _attackEvents.AttackEnded += _attacker.OnAttackEndedEvent;
        _attackEvents.DealDamage += _attacker.Attack;
    }

    private void OnDisable()
    {
        _health.OnDied -= OnDied;
        _collisionHandler.InteractStarted -= OnInteractStarted;
        _attackEvents.AttackEnded -= _attacker.OnAttackEndedEvent;
        _attackEvents.DealDamage -= _attacker.Attack;
    }

    private void FixedUpdate()
    {
        _animator.SetIsWalk(_inputReader.Direction.x != 0
                           || _inputReader.Direction.y != 0);

        if (_inputReader.Direction != null && !_attacker.IsAttack)
        {
            _mover.Move(_inputReader.Direction);
            _fliper.LookAtTarget((Vector2)transform.position + Vector2.right * _inputReader.Direction);
        }

        if (_inputReader.GetIsDash()
            && _inputReader.Direction != null)
        {
            _mover.Stop();
            _mover.Dash(_inputReader.Direction);
            _animator.SetDashTrigger();
        }

        if (_inputReader.GetIsAttack() && _attacker.CanAttack)
        {
            _attacker.StartAttack();
            _animator.SetAttackTrigger();
            _mover.Stop();
            _mover.AttackStep();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnInteractStarted(IInteractable interactableObject)
    {
        _interactable = interactableObject;
    }

    public void ApplyDamage(int damage, Vector2 damageSource)
    {
        _health.ApplyDamage(damage);
        _punch.Punch(damageSource);
        Debug.Log($"Player: {_health.HealthCurrent}");
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
    }

    public void ApplyDamageWithKnockback(int damage, Vector2 pushDirection)
    {
        _health.ApplyDamage(damage);
        _mover.TakeDamage(pushDirection);
    }

    public void Heal(int value)
    {
        _health.Heal(value);
    }
    private void OnDied()
    {
        OnPlayerDied?.Invoke();
    }
}
