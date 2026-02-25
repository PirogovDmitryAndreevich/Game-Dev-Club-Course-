using UnityEngine;

[RequireComponent(typeof(CollisionHandler), typeof(InputReader), typeof(PlayerAttacker))]
[RequireComponent(typeof(CameraShake), typeof(PlayerSounds))]
public class Player : Character
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private InteractableCanvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private PlayerFX _playerFX;

    private InputReader _inputReader;
    private CollisionHandler _collisionHandler;
    private CameraShake _camera;
    private PlayerSounds _sound;

    private Inventory _inventory;

    private IInteractable _interactable;

    private void OnEnable()
    {
        _inventory.ItemAdded += _inventoryView.Add;
        _inventory.ItemRemoved += _inventoryView.Remove;
    }

    private void Start()
    {
        if (SaveData.IsLoaded)
            InitializeHealth();
        else
            SaveData.Loaded += InitializeHealth;
    }

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        base.ApplyDamage(damageInfo, damageSource, pushDirection);
        _sound.PlayHitSound();
    }

    protected override void CharacterAwake()
    {
        Attacker = GetComponent<PlayerAttacker>();
        _inputReader = GetComponent<InputReader>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _camera = GetComponent<CameraShake>();
        _sound = GetComponent<PlayerSounds>();
        Mover = GetComponent<Mover>();
        Animator = GetComponent<CharacterAnimator>();
        Fliper = GetComponent<Fliper>();

        _inventory = new Inventory();

        _collisionHandler.InteractStarted += OnInteractStarted;
        _collisionHandler.ShowingHindePressF += _interactableCanvas.ShowKeyF;
        _collisionHandler.HideHindPressF += _interactableCanvas.HideKeyF;
        _collisionHandler.InteractWithItem += OnInteractWithItem;
        AnimationEvent.DealDamage += Attacker.Attack;
        AnimationEvent.AttackEnded += Attacker.OnAttackEndedEvent;
    }

    protected override void CharacterDestroy()
    {
        base.CharacterDestroy();
        _collisionHandler.InteractStarted -= OnInteractStarted;
        _collisionHandler.InteractWithItem -= OnInteractWithItem;
        _collisionHandler.ShowingHindePressF -= _interactableCanvas.ShowKeyF;
        _collisionHandler.HideHindPressF -= _interactableCanvas.HideKeyF;

        _inventory.ItemAdded -= _inventoryView.Add;
        _inventory.ItemRemoved -= _inventoryView.Remove;
    }

    protected override void CharacterFixUpdate()
    {
        Animator.SetIsWalk(_inputReader.Direction.x != 0
                            || _inputReader.Direction.y != 0);

        if (_inputReader.Direction != null && !Attacker.IsAttack)
        {
            Mover.Move(_inputReader.Direction);
            Fliper.LookAtTarget((Vector2)transform.position + Vector2.right * _inputReader.Direction);

            if (_inputReader.Direction != Vector2.zero)
                _sound.PlayStepsSound();
        }

        if (_inputReader.GetIsDash()
        && _inputReader.Direction != null)
        {
            Mover.Stop();
            Mover.Dash(_inputReader.Direction);
            Animator.SetPlayerDashTrigger();
        }

        if (_inputReader.GetIsAttack() && Attacker.CanAttack)
        {
            Attacker.StartAttack(_camera);
            Animator.SetDefaultPlayerAttackTrigger();
            Mover.Stop();
            Mover.AttackStep();
            _sound.PlayAttackSound();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            Interact();
    }

    private void OnInteractStarted(IInteractable interactableObject)
    {        
            _interactable = interactableObject;
    }

    private void Interact()
    {
        if (_interactable == null)
            return;

        if (_interactable is Defense defense)
            AddArmor((Defense)_interactable);

        if (_interactable is MedKit medKit)
            Heal((MedKit)_interactable);

        if (_interactable is Lock)
            if (!CheckUnlock((Lock)_interactable))
                return;

        _interactable.Interact();
    }

    private void OnInteractWithItem(IItem item)
    {
        if (item is Key)
            AddKey((Key)item);

        if (item is Trophy)
            AddTrophy((Trophy)item);

        item.Collect();
    }

    private void InitializeHealth()
    {
        SaveData.Loaded -= InitializeHealth;

        MaxHealth = SaveData.PlayerData.Health;
        var defense = SaveData.PlayerData.Defense;
        var isShield = defense > 0;

        Health = new Health(MaxHealth, defense, isShield);
        Health.Died += OnDied;

        _healthBar.Initialize(Health);
    }

    private bool CheckUnlock(Lock currentLock)
    {
        if (_inventory.Contains(currentLock.Key))
        {
            _inventory.Remove(currentLock.Key);
            return true;
        }

        return false;
    }

    private void Heal(MedKit medKit)
    {
        _sound.PlaySound(medKit.MedKitSound);
        _playerFX.PlayHeal();
        Health.Heal(medKit.Value);
    }

    private void AddArmor(Defense defense)
    {
        _sound.PlaySound(defense.DefenseSound);
        _playerFX.PlayAddingArmor();
        Health.AddDefense(defense.Value);
    }

    private void AddTrophy(Trophy trophy)
    {
       
    }

    private void AddKey(Key key)
    {
        _inventory.Add(key);
    }
}
