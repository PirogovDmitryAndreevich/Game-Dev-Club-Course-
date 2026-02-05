using UnityEngine;

class FollowState : State, IMoveState
{
    private EnemyDirectionOfView _view;
    private Transform _target;
    private Mover _mover;
    private Fliper _fliper;
    private CharacterAnimator _animator;
    private EnemyAI _enemyAI;
    private EnemySounds _sounds;
    private float _repathCooldown = 0.5f;
    private float _repathTimer;
    private float _closeDistance = 1f;

    public FollowState(StateMachine stateMachine, EnemyAI enemyAI, CharacterAnimator animator,
        Fliper fliper, Mover mover, EnemyDirectionOfView view, float tryFindTime,
        float sqrAttackDistance, EnemySounds sounds) : base(stateMachine)
    {
        _animator = animator;
        _view = view;
        _mover = mover;
        _fliper = fliper;
        _enemyAI = enemyAI;
        _sounds = sounds;

        Transitions = new Transition[]
        {
            new LostTargetTransition(stateMachine, view, tryFindTime),
            new TargetReachedTransition(stateMachine, this, sqrAttackDistance, _mover.transform)
        };
    }

    public Transform Target => _target;

    public override void Enter()
    {
        _view.TrySeeTarget(out _target);
        _animator.SetIsWalk(true);
    }

    public override void Exit()
    {
        _animator.SetIsWalk(false);
    }

    public override void Update()
    {
        if (_target == null)
            return;

        _repathTimer -= Time.deltaTime;

        float sqrDistanceToTarget =
            (_target.position - _mover.transform.position).sqrMagnitude;

        if (sqrDistanceToTarget > _closeDistance * _closeDistance &&
            _repathTimer <= 0f)
        {
            _enemyAI.BuildPath(_target);
            _repathTimer = _repathCooldown;
        }

        Vector3 moveTarget;

        if (sqrDistanceToTarget > _closeDistance * _closeDistance &&
            _enemyAI.HasPath)
        {
            moveTarget = _enemyAI.CurrentPoint;
            _enemyAI.AdvanceIfReached();
        }
        else
        {
            moveTarget = _target.position;
        }

        _mover.Run(moveTarget);
        _sounds.PlayStepsSound();
        _fliper.LookAtTarget(_target.position);
    }
}
