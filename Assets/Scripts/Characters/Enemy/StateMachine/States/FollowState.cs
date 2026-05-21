using UnityEngine;

class FollowState : State, IMoveState
{
    private Transform _target;
    private Enemy _enemy;
    private float _repathCooldown = 0.5f;
    private float _repathTimer;
    private float _closeDistance = 1f;

    public FollowState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _enemy = enemy;

        Transitions = new Transition[]
        {
            new LostTargetTransition(stateMachine, enemy),
            new TargetReachedTransition(stateMachine, this, enemy)
        };
    }

    public Transform Target => _target;

    public override void Enter()
    {
        _enemy.View.TrySeeTarget(out _target);
        _enemy.EnemyAnimator.SetIsWalk(true);
    }

    public override void Exit()
    {
        _enemy.EnemyAnimator.SetIsWalk(false);
    }

    public override void Update()
    {
        if (_target == null)
            return;

        _repathTimer -= Time.deltaTime;

        float sqrDistanceToTarget =
            (_target.position - _enemy.EnemyMover.transform.position).sqrMagnitude;

        if (sqrDistanceToTarget > _closeDistance * _closeDistance &&
            _repathTimer <= 0f)
        {
            _enemy.AI.BuildPath(_target);
            _repathTimer = _repathCooldown;
        }

        Vector3 moveTarget;

        if (sqrDistanceToTarget > _closeDistance * _closeDistance &&
            _enemy.AI.HasPath)
        {
            moveTarget = _enemy.AI.CurrentPoint;
            _enemy.AI.AdvanceIfReached();
        }
        else
        {
            moveTarget = _target.position;
        }

        _enemy.EnemyMover.Run(moveTarget);
        _enemy.Sound.PlayStepsSound();
        _enemy.EnemyFliper.LookAtTarget(_target.position);
    }
}
