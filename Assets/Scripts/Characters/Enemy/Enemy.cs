using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyAnimator), typeof(Fliper))]
[RequireComponent(typeof(EnemyDirectionOfView), typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private EnemyAnimationEvent _animationEvent;
    [SerializeField] private float _waitTime = 2.0f;
    [SerializeField] private float _tryFindTime = 1f;
    [SerializeField] private int _maxHealth = 100;

    private Health _health;
    private float _maxSqrDistance = 14.7f;
    private EnemyAttacker _attacker;
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _attacker = GetComponent<EnemyAttacker>();
        _health = new Health(_maxHealth);
        _animationEvent.Attack += _attacker.Attack;
        _animationEvent.EndAttack += _attacker.OnEndAttackEvent;
    }

    private void Start()
    {
        var animator = GetComponent<EnemyAnimator>();
        var mover = GetComponent<Mover>();
        var fliper = GetComponent<Fliper>();
        var view = GetComponent<EnemyDirectionOfView>();        
        _stateMachine = new EnemyStateMachine(fliper, mover, view, _wayPoints, animator, _maxSqrDistance,
            transform, _waitTime, _tryFindTime, _attacker);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    private void OnDestroy()
    {
        _animationEvent.Attack -= _attacker.Attack;
        _animationEvent.EndAttack -= _attacker.OnEndAttackEvent;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.HealthCurrent <= 0)
            Destroy(gameObject);
    }
    
}





