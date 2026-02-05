using UnityEngine;

class PatrolState : State, IMoveState
{
    private WayPoint[] _wayPoints;

    private Fliper _fliper;
    private Mover _mover;
    private Transform _target;
    private CharacterAnimator _animator;
    private EnemyAI _enemyAI;
    private EnemySounds _sounds;
    private int _wayPointIndex;

    public PatrolState(StateMachine stateMachine, EnemyAI enemyAI, Fliper fliper, Mover mover, EnemyDirectionOfView view,
                       WayPoint[] wayPoints, float maxSqrDistance, Transform transform,
                       CharacterAnimator animator, float sqrAttackDistance, EnemySounds sounds) : base(stateMachine)
    {
        _animator = animator;
        _fliper = fliper;
        _mover = mover;
        _wayPoints = wayPoints;
        _wayPointIndex = -1;
        _enemyAI = enemyAI;
        _sounds = sounds;

        var targetReachedTransition = new WayPointReachedTransition(stateMachine, this, maxSqrDistance, transform);
        targetReachedTransition.Transiting += ChangeTarget;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, view, transform, sqrAttackDistance),
            targetReachedTransition
        };

        ChangeTarget();
    }

    public Transform Target => _target;

    public override void Enter()
    {        
        _enemyAI.BuildPath(_target);
        _animator.SetIsWalk(true);
    }

    public override void Exit()
    {
        _animator.SetIsWalk(false);
    }

    public override void Update()
    {
        Vector2 target;

        if (_enemyAI.HasPath)
        {
            target = _enemyAI.CurrentPoint;
            _enemyAI.AdvanceIfReached();
        }
        else
        {
            target = _target.position;
        }

        _fliper.LookAtTarget(_target.position);
        _mover.Walk(target);
        _sounds.PlayStepsSound();
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;
    }
}