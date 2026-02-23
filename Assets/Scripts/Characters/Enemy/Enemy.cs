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
    protected EnemySounds Sound;

    private void Start()
    {
        var view = GetComponent<EnemyDirectionOfView>();
        var enemyAI = GetComponent<EnemyAI>();
        Sound = GetComponent<EnemySounds>();

        _stateMachine = new EnemyStateMachine(Fliper, Mover, view, _wayPoints, Animator, _maxSqrDistance,
            transform, _waitTime, _tryFindTime, Attacker, enemyAI, Sound);
    }

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        Sound.PlayHitSound();

        base.ApplyDamage(damageInfo, damageSource, pushDirection);

        var damageNumber = FXPool.Get(FXType.DamageNumber) as DamageValueAnimation;
        damageNumber.Play(damageSource, damageInfo.Damage, damageInfo.IsCrit);

        if (Health.HealthCurrent <= 0)
        {
            Mover.Stop();
            Sound.PlayDeathSound();
            var deathParticles = FXPool.Get(FXType.EnemyDeath);
            deathParticles.Play(transform.position);
            Destroy(gameObject);
        }
    }

    protected override void CharacterFixUpdate()
    {
        _stateMachine.Update();
    }

    protected override void OnDied()
    {
        OnEnemyDied?.Invoke();
    }
}