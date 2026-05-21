using System;
using UnityEngine;

[RequireComponent(typeof(EnemyDirectionOfView), typeof(EnemyAI))]
public class Enemy : Character, ITask
{
    //[SerializeField] private RewardsSpawner _rewardsSpawner;
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private EnemySounds _sounds;
    [SerializeField] private EnemyDirectionOfView _view;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private int _rewardCoins;
    [SerializeField] private int _rewardGems;

    public event Action<Enemy> EnemyDied;
    public event Action<ITask> TaskCompleted;

    private EnemyStateMachine _stateMachine;

    public CharacterAnimator EnemyAnimator => Animator;
    public Fliper EnemyFliper => Fliper;
    public Mover EnemyMover => Mover;
    public EnemyAI AI => _enemyAI;
    public EnemySounds Sound => _sounds;
    public EnemyDirectionOfView View => _view;
    public Attacker Attacker => _attacker;
    public WayPoint[] WayPoints { get; private set; }
    public EnemyHealth Health { get; private set; }
    public EnemyStaticData StaticData { get; private set; }
    public TaskType Type => TaskType.Enemies;

    public void Construct(EnemyStaticData data)
    {
        StaticData = data;
        Health = new EnemyHealth(StaticData);
        _stateMachine = new EnemyStateMachine(this);
    }

    public override void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        Sound.PlayHitSound();

        base.ApplyDamage(damageInfo, damageSource, pushDirection);

        //var damageNumber = FXPool.Get(FXType.DamageNumber) as DamageValueAnimation;
        //damageNumber.Play(damageSource, damageInfo.Damage, damageInfo.IsCrit);

        if (Health.HealthCurrent <= 0)
        {
            Mover.Stop();

            Sound.PlayDeathSound();
            //var deathParticles = FXPool.Get(FXType.EnemyDeath);
            //deathParticles.Play(transform.position);

            //SpawnRewards();

            Destroy(gameObject);
        }
    }

    private void FixedUpdate() => 
        _stateMachine.Update();

    private void OnDied()
    {
        TaskCompleted?.Invoke(this);
        EnemyDied?.Invoke(this);
    }
}