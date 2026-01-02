using UnityEngine;

class SeeTargetTransition : Transition
{
    EnemyDirectionOfView _view;
    Transform _transform;
    private float _sqrAttackDistance;

    public SeeTargetTransition(StateMachine stateMachine, EnemyDirectionOfView view, Transform transform, float sqrAttackDistance) : base(stateMachine)
    {
        _view = view;
        _transform = transform;
        _sqrAttackDistance = sqrAttackDistance;
    }

    public override bool IsNeedTransit() => _view.TrySeeTarget(out Transform target) 
        && (_transform.position - target.position).sqrMagnitude > _sqrAttackDistance;

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<FollowState>();
    }
}





