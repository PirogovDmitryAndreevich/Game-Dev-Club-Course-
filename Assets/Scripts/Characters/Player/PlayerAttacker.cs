using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : Attacker
{
    [Header("Player attacker")]
    [SerializeField] private int _superHitCount = 3;
    [SerializeField] private float _comboCooldown = 1.2f;

    public override bool CanAttack => !_isAttacking;

    public override AttacksType type => _attack.Type;

    private AttackBase _defaultAttack;
    private List<AttackBase> _superAttacks = new();
    private Collider2D[] _hits = new Collider2D[16];

    private bool _isAttacking = false;
    private int _hitCounter;
    private float _lastHitTime;

    protected override void Awake()
    {
        base.Awake();
        _defaultAttack = AttacksData.Attacks[AttacksType.PlayerDefaultAttack];
        _superAttacks.Add(AttacksData.Attacks[AttacksType.PlayerSuperAttack]);
        _attack = _defaultAttack;
    }

    public override void StartAttack(CameraShake camera)
    {
        base.StartAttack(camera);
        _isAttacking = true;
    }

    public override void Attack()
    {
        Vector2 origin = GetAttackOrigin();

        bool isSuperHit = RegisterHit();

        int countHits = Physics2D.OverlapCircleNonAlloc(origin, _radius, _hits, _targetLayer);

        AttackBase attack = isSuperHit
            ? _superAttacks[UnityEngine.Random.Range(0, _superAttacks.Count)]
            : _attack;

        if (isSuperHit)
        {
            _camera.ShakeSuperPunch();
        }
        else
        {
            _camera.ShakePunch();
        }

        for (int i =0; i < countHits; i++)
        {
            var hit = _hits[i];

            if (hit != null && hit.TryGetComponent(out Enemy enemy))
            {
                Vector2 pushDirection = _fliper.IsTernRight
                                        ? Vector2.right
                                            : Vector2.left;

                enemy.ApplyDamage(attack, hit.ClosestPoint(origin), pushDirection);
            }
        }        
    }

    public override void OnAttackEndedEvent()
    {
        base.OnAttackEndedEvent();
        _isAttacking = false;
    }

    private bool RegisterHit()
    {
        float time = Time.time;

        if (time - _lastHitTime > _comboCooldown)
        {
            _hitCounter = 0;
        }

        _lastHitTime = time;
        _hitCounter++;

        if (_hitCounter >= _superHitCount)
        {
            _hitCounter = 0;
            return true; 
        }

        return false;
    }

    internal bool TrySeeTarget(out object target)
    {
        throw new NotImplementedException();
    }
}

/*if (countHits != null && countHits.TryGetComponent(out Enemy enemy))
{
    Vector2 pushDirection = _fliper.IsTernRight
                                ? Vector2.right
                                    : Vector2.left;

    enemy.ApplyDamage(attack, countHits.ClosestPoint(origin), pushDirection);
}
*/
