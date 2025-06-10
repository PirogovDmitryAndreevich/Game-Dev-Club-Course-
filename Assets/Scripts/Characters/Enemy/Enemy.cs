using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyAnimator), typeof(Fliper))]
[RequireComponent(typeof(EnemyDirectionOfView))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _waitTime = 2.0f;
    [SerializeField] private float _tryFindTime = 1f;
    [SerializeField] private int _maxHealth = 100;

    private Health _health;
    private EnemyAnimator _animator;
    private float _maxSqrDistance = 14.7f;
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _health = new Health(_maxHealth);
    }

    private void Start()
    {
        _animator = GetComponent<EnemyAnimator>();
        var mover = GetComponent<Mover>();
        var fliper = GetComponent<Fliper>();
        var view = GetComponent<EnemyDirectionOfView>();
        _stateMachine = new EnemyStateMachine(fliper, mover, view, _wayPoints, _animator, _maxSqrDistance, transform, _waitTime, _tryFindTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage);

        if (_health.HealthCurrent <= 0)
            Destroy(gameObject);
    }
    
}





