using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyDirectionOfView), typeof(EnemyAI))]
public class Enemy : Character
{
    [SerializeField] private RewardsSpawner _rewardsSpawner;
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemySounds _sounds;
    [SerializeField] private EnemyDirectionOfView _view;
    [SerializeField] private EnemyAttacker _attacker;
    [SerializeField] private int _rewardCoins;
    [SerializeField] private int _rewardGems;

    public event Action<Enemy> EnemyDied;

    private EnemyStateMachine _stateMachine;
    private IPoolService _pool;

    public CharacterAnimator EnemyAnimator => Animator;
    public Fliper EnemyFliper => Fliper;
    public Mover EnemyMover => Mover;
    public EnemyAI AI => _enemyAI;
    public EnemySounds Sound => _sounds;
    public EnemyDirectionOfView View => _view;
    public EnemyAttacker Attacker => _attacker;
    public RewardsSpawner RewardsSpawner => _rewardsSpawner;
    public WayPoint[] WayPoints { get; private set; }
    public EnemyHealth Health { get; private set; }
    public EnemyStaticData StaticData { get; private set; }

    private void OnDestroy() => 
        Health.Died -= OnDied;

    public void Construct(EnemyStaticData data, List<WayPoint> wayPoints, IPoolService poolService)
    {
        StaticData = data;
        WayPoints = wayPoints.ToArray();
        Health = new EnemyHealth(StaticData);
        _stateMachine = new EnemyStateMachine(this);

        _pool = poolService;

        Health.Died += OnDied;
    }

    public override void ApplyDamage(int damage, float knockbackForce, Vector2 damageSource, Vector2 pushDirection)
    {
        Sound.PlayHitSound();

        base.ApplyDamage(damage, knockbackForce, damageSource, pushDirection);

        PunchAnimation punch = _pool.Get<PunchAnimation>();
        punch.Play(damageSource);

        DamageValueAnimation damageNumber = _pool.Get<DamageValueAnimation>();
        damageNumber.Play(damageSource, damage);

        Health.ApplyDamage(damage);
    }

    private void FixedUpdate() => 
        _stateMachine.Update();

    private void OnDied()
    {
        EnemyDied?.Invoke(this);

        Mover.Stop();

        Sound.PlayDeathSound();
        EnemyDeathParticles deathParticles = _pool.Get<EnemyDeathParticles>();
        deathParticles.Play(transform.position);

        RewardsSpawner.CreateCoins(StaticData.Reward);

        if (UnityEngine.Random.Range(0f, 100f) < StaticData.GemPercent)
            RewardsSpawner.CreateGems();

        Destroy(gameObject);
    }
}