using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputReader), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandler), typeof(Fliper), typeof(PlayerAttacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 20;

    private Mover _mover;
    private InputReader _inputReader;
    private PlayerAnimator _animator;
    private CollisionHandler _collisionHandler;
    private Fliper _fliper;
    private PlayerAttacker _attacker;
    private Health _health;

    private IInteractable _interactable;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _attacker = GetComponent<PlayerAttacker>();
        _mover = GetComponent<Mover>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _fliper = GetComponent<Fliper>();
    }

    private void OnEnable()
    {
        _collisionHandler.InteractStarted += OnInteractStarted;
    }

    private void OnDisable()
    {
        _collisionHandler.InteractStarted -= OnInteractStarted;
    }

    private void FixedUpdate()
    {
        _animator.SetIsWalk(_inputReader.Direction.x != 0
                           || _inputReader.Direction.y != 0);

        if (_inputReader.Direction != null)
        {
            _mover.Move(_inputReader.Direction);
            _fliper.LookAtTarget((Vector2)transform.position + Vector2.right * _inputReader.Direction);
        }

        if (_inputReader.GetIsDash() && _inputReader.Direction != null)
        {
            _mover.Dash(_inputReader.Direction);
            _animator.SetDashTrigger();
        }

        if (_inputReader.GetIsAttack() && _attacker.CanAttack)
        {
            _attacker.StartAttack();
            _attacker.Attack();
            _animator.SetAttackTrigger();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnInteractStarted(IInteractable interactableObject)
    {
        _interactable = interactableObject;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);
        _animator.SetHitTrigger();
        Debug.Log($"Player: {_health.HealthCurrent}");
    }

    public void Heal(int value)
    {
        _health.Heal(value);
    }
}
