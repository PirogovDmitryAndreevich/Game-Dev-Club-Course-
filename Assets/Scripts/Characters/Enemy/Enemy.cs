using UnityEngine;

[RequireComponent(typeof(Mover), typeof(EnemyAnimator), typeof(Fliper))]
[RequireComponent(typeof(EnemyDirectionOfView))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _waitTime = 2.0f;
    [SerializeField] private float _tryFindTime = 1f;
    
    private EnemyAnimator _animator;
    private float _maxSqrDistance = 14.7f;
    private EnemyStateMachine _stateMachine;

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
}





