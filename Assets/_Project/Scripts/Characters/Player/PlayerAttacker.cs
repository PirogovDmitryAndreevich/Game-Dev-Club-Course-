using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttacker : Attacker
{
    [Header("Player attacker")]
    [SerializeField] private AnimationEvent _attackEvent;
    [SerializeField] protected CharacterAnimator _animator;
    [SerializeField] private CameraShake _cameraShake;

    [Header("Editor Settings")]
    [SerializeField] private PlayerStaticData _playerStaticData;
    [SerializeField] private PlayerAttackType _attackType;

    private IPersistentProgressService _progress;
    private PlayerStaticData _staticData;

    private List<AttackData> _availableSuperAttacks = new();
    private Collider2D[] _hits = new Collider2D[16];

    private Dictionary<PlayerAttackType, AttackData> _attacksByType;

    private AttackData _defaultAttack;
    private AttackData _currentAttack;

    private int _hitCounter;
    private float _lastHitTime;

    public override float CooldownTime => _staticData.CooldownTime;
    public override float Offset => _defaultAttack.AttackOffset;
    public override float Radius => _defaultAttack.AttackRadius;

    private void OnValidate()
    {
        if (_playerStaticData == null)
            return;

        _defaultAttack = _playerStaticData.Attacks
            .FirstOrDefault(a => a.Type == _attackType);
    }

    private void OnDrawGizmos()
    {
        if (_defaultAttack == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), _defaultAttack.AttackRadius);
    }

    private void OnDestroy()
    {
        _attackEvent.AttackEnded -= OnEndedAttackEvent;
        _attackEvent.DealDamage -= OnDealDamage;
    }

    private void Start()
    {
        _attackEvent.AttackEnded += OnEndedAttackEvent;
        _attackEvent.DealDamage += OnDealDamage;
    }

    public void Construct(PlayerStaticData data, IPersistentProgressService progressService)
    {
        _progress = progressService;
        _staticData = data;

        _availableSuperAttacks.Clear();

        PlayerAttacksData attacksData =
            _progress.Progress.PlayerAttacksData;

        _defaultAttack = _staticData.Attacks
            .FirstOrDefault(x => x.Type == PlayerAttackType.Default);

        if (!attacksData.FirstSlot.IsEmpty)
            AddSuperAttack(attacksData.FirstSlot.Type);

        if (!attacksData.SecondSlot.IsEmpty)
            AddSuperAttack(attacksData.SecondSlot.Type);

        _currentAttack = _defaultAttack;
    }

    public override void Attack()
    {
        SelectAttack();
        _animator.SetPlayerAttack(_currentAttack.Type);
    }

    private void OnDealDamage()
    {
        if (_currentAttack == null)
            return;

        Vector2 origin = GetAttackOrigin();

        int countHits = Physics2D.OverlapCircleNonAlloc(origin, _currentAttack.AttackRadius, _hits, TargetLayer);

        if (_currentAttack.IsCreat)
            _cameraShake.ShakeSuperPunch();
        else
            _cameraShake.ShakePunch();

        for (int i = 0; i < countHits; i++)
        {
            var hit = _hits[i];

            if (hit == null)
                continue;

            if (hit.TryGetComponent(out Enemy enemy))
            {
                Vector2 pushDirection = Fliper.IsTernRight
                                        ? Vector2.right
                                        : Vector2.left;

                enemy.ApplyDamage(_currentAttack.Damage[0].Damage, _currentAttack.KnockbackForce, hit.ClosestPoint(origin), pushDirection);
            }
        }
    }

    private void OnEndedAttackEvent() =>
        EndedAttack();

    private void SelectAttack()
    {
        bool isSuperHit = RegisterHit();

        _currentAttack = _defaultAttack;

        if (isSuperHit && _availableSuperAttacks.Count > 0)
        {
            _currentAttack = _availableSuperAttacks[
                Random.Range(0, _availableSuperAttacks.Count)];
        }
    }

    private void AddSuperAttack(PlayerAttackType type)
    {
        if (_attacksByType.TryGetValue(type, out AttackData attack) &&
            attack.Type != PlayerAttackType.Default)
        {
            _availableSuperAttacks.Add(attack);
        }
    }

    private bool RegisterHit()
    {
        float time = Time.time;

        if (time - _lastHitTime > _staticData.ComboCooldown)
        {
            _hitCounter = 0;
        }

        _lastHitTime = time;
        _hitCounter++;

        if (_hitCounter >= _staticData.SuperHitCount)
        {
            _hitCounter = 0;
            return true;
        }

        return false;
    }
}
