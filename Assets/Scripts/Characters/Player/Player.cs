using UnityEngine;

[RequireComponent(typeof(CollisionHandler), typeof(InputReader), typeof(PlayerAttacker))]
[RequireComponent(typeof(CameraShake), typeof(PlayerSounds))]
public class Player : Character
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private InteractableCanvas _interactableCanvas;
    [SerializeField] private ParticleSystem _healEffect; 
    [SerializeField] private ParticleSystem _defenseEffect;

    private InputReader _inputReader;
    private CollisionHandler _collisionHandler;
    private CameraShake _camera;
    private PlayerSounds _sound;

    private IInteractable _interactable;

    protected override void Awake()
    { 
        _attacker = GetComponent<PlayerAttacker>();
        _inputReader = GetComponent<InputReader>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _camera = GetComponent<CameraShake>();
        _sound = GetComponent<PlayerSounds>();
        _mover = GetComponent<Mover>();
        _animator = GetComponent<CharacterAnimator>();
        _fliper = GetComponent<Fliper>();

        _collisionHandler.InteractStarted += OnInteractStarted;
        _collisionHandler.OnShowKeyF += _interactableCanvas.ShowKeyF;
        _collisionHandler.OnHideKeyF += _interactableCanvas.HideKeyF;
        _animationEvent.DealDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEndedEvent;
        
    }

    private void Start()
    {
        if (SaveData.IsLoaded)
            InitializeHealth();
        else
            SaveData.OnLoaded += InitializeHealth;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _collisionHandler.InteractStarted -= OnInteractStarted;
        _collisionHandler.OnShowKeyF -= _interactableCanvas.ShowKeyF;
        _collisionHandler.OnHideKeyF -= _interactableCanvas.HideKeyF;
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

    public void Heal(MedKit medKit)
    {
            _healEffect.Play();
            _health.Heal(medKit.Value); 
    }

    public void AddArmor(Defense defense)
    {
        _defenseEffect.Play();
        _health.AddDefense(defense.Value);
    }

    private void InitializeHealth()
    {
        SaveData.OnLoaded -= InitializeHealth;

        _maxHealth = SaveData.PlayerData.Health;
        var defense = SaveData.PlayerData.Defense;
        var isShield = defense > 0;

        _health = new Health(_maxHealth, defense, isShield);
        _health.OnDied += OnDied;

        _healthBar.Initialize(_health);
    }
}
