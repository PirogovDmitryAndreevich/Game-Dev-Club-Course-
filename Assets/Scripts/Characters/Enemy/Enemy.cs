using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyAnimator), typeof(Fliper))]
[RequireComponent(typeof(EnemyDirectionOfView))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private EnemyAnimationEvent _animationEvent;
    [SerializeField] private float _waitTime = 2.0f;
    [SerializeField] private float _tryFindTime = 1f;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _maxSqrDistance = 13.7f;

    protected EnemyAttacker _attacker;

    private Health _health;
    private EnemyStateMachine _stateMachine;

    protected virtual void Awake()
    {
        _health = new Health(_maxHealth);
        _animationEvent.DealDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEndedEvent;
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
        _animationEvent.DealDamage -= _attacker.Attack;
        _animationEvent.AttackEnded -= _attacker.OnAttackEndedEvent;
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        Debug.Log($"Enemy: {_health.HealthCurrent}");

        if (_health.HealthCurrent <= 0)
            Destroy(gameObject);
    }
    
}





