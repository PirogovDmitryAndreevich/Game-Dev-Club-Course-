using System;
using UnityEngine;

[RequireComponent(typeof(EnemyDirectionOfView), typeof(Attacker), typeof(EnemyAI))]
public class Enemy : Character
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _waitTime = 2.0f;
    [SerializeField] private float _tryFindTime = 1f;
    [SerializeField] private float _maxSqrDistance = 13.7f;

    public event Action OnEnemyDied;

    private EnemyStateMachine _stateMachine;
    protected EnemySounds _sound;

    private void Start()
    {
        var view = GetComponent<EnemyDirectionOfView>();
        var enemyAI = GetComponent<EnemyAI>();
        _sound = GetComponent<EnemySounds>();

        _stateMachine = new EnemyStateMachine(_fliper, _mover, view, _wayPoints, _animator, _maxSqrDistance,
            transform, _waitTime, _tryFindTime, _attacker, enemyAI, _sound);
    }

    protected override void FixedUpdate() => _stateMachine.Update(); 

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        _sound.PlayHitSound();

        base.ApplyDamage(damageInfo, damageSource, pushDirection);

        var damageNumber = FXPool.Instance.Get(FXType.DamageNumber) as DamageValueAnimation;
        damageNumber.Play(damageSource, damageInfo.Damage, damageInfo.IsCrit);

        if (_health.HealthCurrent <= 0)
        {
            _mover.Stop();
            _sound.PlayDeathSound();
            var deathParticles = FXPool.Instance.Get(FXType.EnemyDeath);
            deathParticles.Play(transform.position);
            Destroy(gameObject);
        }
    }

    protected override void OnDied()
    {
        OnEnemyDied?.Invoke();
    }
}