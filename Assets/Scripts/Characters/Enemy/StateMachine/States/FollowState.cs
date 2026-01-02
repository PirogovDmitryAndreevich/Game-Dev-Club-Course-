using UnityEngine;

class FollowState : State, IMoveState
{
    private EnemyDirectionOfView _view;
    private Transform _target;
    private Mover _mover;
    private Fliper _fliper;
    private EnemyAnimator _animator;

    public FollowState(StateMachine stateMachine, EnemyAnimator animator, Fliper fliper, Mover mover,
        EnemyDirectionOfView view, float tryFindTime, float sqrAttackDistance) : base(stateMachine)
    {
        _animator = animator;
        _view = view;
        _mover = mover;
        _fliper = fliper;

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
        if (_target != null)
        {
            _mover.Run(_target);
            _fliper.LookAtTarget(_target.position);
        }
    }
}





