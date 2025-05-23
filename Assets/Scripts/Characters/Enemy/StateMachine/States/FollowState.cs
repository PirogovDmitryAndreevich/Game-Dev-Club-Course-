using UnityEngine;

class FollowState : State
{
    private EnemyDirectionOfView _view;
    private Transform _target;
    private Mover _mover;
    private Fliper _fliper;
    private EnemyAnimator _animator;

    public FollowState(StateMachine stateMachine, EnemyAnimator animator, Fliper fliper, Mover mover, EnemyDirectionOfView view, float tryFindTime) : base(stateMachine)
    {
        _animator = animator;
        _view = view;
        _mover = mover;
        _fliper = fliper;

        Transitions = new Transition[]
        {
            new LostTargetTransition(stateMachine, view, tryFindTime)
        };
    }

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





