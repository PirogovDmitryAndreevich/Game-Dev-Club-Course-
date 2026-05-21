using UnityEngine;

class PatrolState : State, IMoveState
{
    private Enemy _enemy;
    private Transform _target;
    private int _wayPointIndex;

    public PatrolState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _enemy = enemy;
        _wayPointIndex = -1;

        var targetReachedTransition = new WayPointReachedTransition(stateMachine, this, enemy);
        targetReachedTransition.Transiting += ChangeTarget;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine,enemy),
            targetReachedTransition
        };

        ChangeTarget();
    }

    public Transform Target => _target;

    public override void Enter()
    {        
        _enemy.AI.BuildPath(_target);
        _enemy.EnemyAnimator.SetIsWalk(true);
    }

    public override void Exit()
    {
        _enemy.EnemyAnimator.SetIsWalk(false);
    }

    public override void Update()
    {
        Vector2 target;

        if (_enemy.AI.HasPath)
        {
            target = _enemy.AI.CurrentPoint;
            _enemy.AI.AdvanceIfReached();
        }
        else
        {
            target = _target.position;
        }

        _enemy.EnemyFliper.LookAtTarget(_target.position);
        _enemy.EnemyMover.Walk(target);
        _enemy.Sound.PlayStepsSound();
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _enemy.WayPoints.Length;
        _target = _enemy.WayPoints[_wayPointIndex].transform;
    }
}