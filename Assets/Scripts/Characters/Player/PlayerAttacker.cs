using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : Attacker
{
    [Header("Player attacker")]
    [SerializeField] private int _superHitCount = 3;
    [SerializeField] private float _comboCooldown = 1.2f;

    private AttackBase _defaultAttack;
    private bool _isAttacking = false;
    private int _hitCounter;
    private float _lastHitTime;

    public override bool CanAttack => !_isAttacking;
    public override AttacksType type => AttackType.Type;

    private List<AttackBase> _superAttacks = new();
    private Collider2D[] _hits = new Collider2D[16];

    public override void StartAttack(CameraShake camera)
    {
        base.StartAttack(camera);
        _isAttacking = true;
    }

    public override void Attack()
    {
        Vector2 origin = GetAttackOrigin();

        bool isSuperHit = RegisterHit();

        int countHits = Physics2D.OverlapCircleNonAlloc(origin, Radius, _hits, TargetLayer);

        AttackBase attack = isSuperHit
            ? _superAttacks[UnityEngine.Random.Range(0, _superAttacks.Count)]
            : AttackType;

        if (isSuperHit)
        {
            Camera.ShakeSuperPunch();
        }
        else
        {
            Camera.ShakePunch();
        }

        for (int i =0; i < countHits; i++)
        {
            var hit = _hits[i];

            if (hit != null && hit.TryGetComponent(out Enemy enemy))
            {
                Vector2 pushDirection = Fliper.IsTernRight
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

    protected override void AttackAwake()
    {
        base.AttackAwake();
        _defaultAttack = AttacksData.Attacks[AttacksType.PlayerDefaultAttack];
        _superAttacks.Add(AttacksData.Attacks[AttacksType.PlayerSuperAttack]);
        AttackType = _defaultAttack;
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
}
