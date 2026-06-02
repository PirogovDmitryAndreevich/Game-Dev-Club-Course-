using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : Attacker
{
    [Header("Player attacker")]
    [SerializeField] private AnimationEvent _attackEvent;
    [SerializeField] protected CharacterAnimator _animator;
    [SerializeField] private CameraShake _cameraShake;

    private PlayerStaticData _staticData;
    private Dictionary<PlayerAttackType, Attack> _attacks;
    private Collider2D[] _hits = new Collider2D[16];

    private int _hitCounter;
    private float _lastHitTime;

    public override float CooldownTime => _staticData.CooldownTime;
    public override float Offset => _currentAttack.AttackOffset;
    public override float Radius => _currentAttack.AttackRadius;
    
    private Attack _currentAttack;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), Radius);
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

    public void Construct(PlayerStaticData data)
    {
        _staticData = data;
        _attacks = new Dictionary<PlayerAttackType, Attack>();

        foreach(var attack in _staticData.Attacks)
            _attacks.Add(attack.Type, attack);

        _currentAttack = _attacks[PlayerAttackType.Default];
    }

    public override void Attack() => 
        _animator.SetPlayerAttack(_currentAttack.Type);

    private void OnDealDamage()
    {
        Vector2 origin = GetAttackOrigin();

        bool isSuperHit = RegisterHit();

        int countHits = Physics2D.OverlapCircleNonAlloc(origin, Radius, _hits, TargetLayer);

        /*AttackBase attack = isSuperHit
            ? _superAttacks[Random.Range(0, _superAttacks.Count)]
            : AttackType;*/

        if (isSuperHit)        
            _cameraShake.ShakeSuperPunch();        
        else        
            _cameraShake.ShakePunch();
        

        for (int i = 0; i < countHits; i++)
        {
            var hit = _hits[i];

            if (hit != null && hit.TryGetComponent(out Enemy enemy))
            {
                Vector2 pushDirection = Fliper.IsTernRight
                                        ? Vector2.right
                                        : Vector2.left;

                enemy.ApplyDamage(_currentAttack.Damage, _currentAttack.KnockbackForce, hit.ClosestPoint(origin), pushDirection);
            }
        }
    }

    private void OnEndedAttackEvent() => 
        EndedAttack();

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
