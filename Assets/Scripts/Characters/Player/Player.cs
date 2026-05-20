using UnityEngine;

[RequireComponent(typeof(CollisionHandler), typeof(InputReader), typeof(PlayerAttacker))]
[RequireComponent(typeof(CameraShake), typeof(PlayerSounds))]
public class Player : Character
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private InteractableCanvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private PlayerFX _playerFX;
    [SerializeField] private ArrowTracking _arrowTracking;

    private IInputServices _input;
    private SaveData _progress;
    private ISaveLoadService _save;
    private InputReader _inputReader;
    private CollisionHandler _collisionHandler;
    private CameraShake _camera;
    private PlayerSounds _sound;
    private bool _isCutscene;

    private Inventory _inventory;

    private IInteractable _interactable;

    private void OnEnable()
    {
        _isCutscene = false;
        _inventory.ItemAdded += _inventoryView.Add;
        _inventory.ItemRemoved += _inventoryView.Remove;
    }

    public void Construct(IPersistentProgressService progress, ISaveLoadService save)
    {
        _progress = progress.Progress;
        _save = save;
        InitializeHealth(_progress);
    }

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        base.ApplyDamage(damageInfo, damageSource, pushDirection);
        _sound.PlayHitSound();
    }

    public void StartMovingInCutscene()
    {
        _isCutscene = true;
        Animator.SetIsWalk(true);
    }

    public void FinishLevelConditionsCompleted()
    {
        _arrowTracking.gameObject.SetActive(true);
    }

    protected override void CharacterAwake()
    {
        _arrowTracking.gameObject.SetActive(false);

        Attacker = GetComponent<PlayerAttacker>();
        _inputReader = GetComponent<InputReader>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _camera = GetComponent<CameraShake>();
        _sound = GetComponent<PlayerSounds>();
        Mover = GetComponent<Mover>();
        Animator = GetComponent<CharacterAnimator>();
        Fliper = GetComponent<Fliper>();

        _input = AllServices.Container.Single<IInputServices>();

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
        if (_isCutscene)
            return;

        /*Animator.SetIsWalk(_inputReader.Direction.x != 0
                            || _inputReader.Direction.y != 0);*/

        Animator.SetIsWalk(_input.Direction.x != 0
            || _input.Direction.y != 0);

        //if (_inputReader.Direction != null && !Attacker.IsAttack)
        if (_input.Direction != null && !Attacker.IsAttack)
        {
            // Mover.Move(_inputReader.Direction);
            Mover.Move(_input.Direction);
            //Fliper.LookAtTarget((Vector2)transform.position + Vector2.right * _inputReader.Direction);
            Fliper.LookAtTarget((Vector2)transform.position + Vector2.right * _input.Direction);

            //if (_inputReader.Direction != Vector2.zero)
            if (_input.Direction != Vector2.zero)
                _sound.PlayStepsSound();
        }

        /* if (_inputReader.GetIsDash()
         && _inputReader.Direction != null)*/
        if (_input.GetIsDash()
        && _input.Direction != null)
        {
            Mover.Stop();
            //Mover.Dash(_inputReader.Direction);
            Mover.Dash(_input.Direction);
            Animator.SetPlayerDashTrigger();
            _sound.PlayDash();
        }

        //if (_inputReader.GetIsAttack() && Attacker.CanAttack)
        if (_input.GetIsAttack() && Attacker.CanAttack)
        {
            Attacker.StartAttack(_camera);
            Animator.SetDefaultPlayerAttackTrigger();
            Mover.Stop();
            Mover.AttackStep();
            _sound.PlayAttackSound();
        }

        // if (_inputReader.GetIsInteract() && _interactable != null)
        if (_input.GetIsInteract() && _interactable != null)
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

        if (item is Trophy trophy)
            _sound.PlaySound(trophy.TrophySound);

        if (item is Coin)
            AddCoin((Coin)item);

        if (item is Gem)
            AddGem((Gem)item);

        item.Collect();
    }

    private void InitializeHealth(SaveData save)
    {
        MaxHealth = save.PlayerData.Health;
        var defense = save.PlayerData.Defense;
        var isShield = defense > 0;

        Health = new Health(MaxHealth, defense, isShield);
        Health.Died += OnDied;

        _healthBar.Initialize(Health);
    }

    private bool CheckUnlock(Lock currentLock)
    {
        if (_inventory.Contains(currentLock.Key))
        {
            _sound.PlaySound(currentLock.UnlockSound);
            _inventory.Remove(currentLock.Key);
            return true;
        }

        _sound.PlaySound(currentLock.NegativeSound);

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

    private void AddKey(Key key)
    {
        _sound.PlaySound(key.KeySound);
        _inventory.Add(key);
    }

    private void AddCoin(Coin coin)
    {
        _progress.PlayerData.SetStat(StatsType.Coins, _progress.PlayerData.Coins + coin.Value);
        _sound.PlaySound(coin.Sound);
        _save.SaveProgress();
        Debug.Log($"Add coin: {coin.Value}");
    }

    private void AddGem(Gem gem)
    {
        _progress.PlayerData.SetStat(StatsType.Gem, _progress.PlayerData.Gems + gem.Value);
        _sound.PlaySound(gem.Sound);
        _save.SaveProgress();
        Debug.Log($"Add gem: {gem.Value}");
    }
}
