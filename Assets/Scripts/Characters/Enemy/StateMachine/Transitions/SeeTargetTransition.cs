using UnityEngine;

class SeeTargetTransition : Transition
{
    EnemyDirectionOfView _view;
    public SeeTargetTransition(StateMachine stateMachine, EnemyDirectionOfView view) : base(stateMachine) => _view = view;

    public override bool IsNeedTransit() => _view.TrySeeTarget(out Transform _);

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChangeState<FollowState>();
    }
}





