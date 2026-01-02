using UnityEngine;

class PatrolState : State, IMoveState
{
    private WayPoint[] _wayPoints;

    private Fliper _fliper;
    private Mover _mover;
    private Transform _target;
    private EnemyAnimator _animator;
    private int _wayPointIndex;

    public PatrolState(StateMachine stateMachine, Fliper fliper, Mover mover, EnemyDirectionOfView view,
                       WayPoint[] wayPoints, float maxSqrDistance, Transform transform,
                       EnemyAnimator animator, float sqrAttackDistance) : base(stateMachine)
    {
        _animator = animator;
        _fliper = fliper;
        _mover = mover;
        _wayPoints = wayPoints;
        _wayPointIndex = -1;

        var targetReachedTransition = new WayPointReachedTransition(stateMachine, this, maxSqrDistance, transform);
        targetReachedTransition.Transiting += ChangeTarget;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, view, transform, sqrAttackDistance),
            targetReachedTransition
        };

        ChangeTarget();
        _animator = animator;
    }

    public Transform Target => _target;

    public override void Enter()
    {
        _fliper.LookAtTarget(_target.position);
        _animator.SetIsWalk(true);
    }

    public override void Exit()
    {
        _animator.SetIsWalk(false);
    }

    public override void Update()
    {
        _mover.Walk(_target);

    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;        
    }
}





