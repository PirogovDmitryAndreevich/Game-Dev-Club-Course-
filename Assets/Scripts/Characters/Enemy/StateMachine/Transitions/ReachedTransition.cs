using UnityEngine;

class ReachedTransition : Transition
{
    private float _maxSqrDistance =0.1f;
    private IMoveState _moveState;
    private Transform _transform;

    public ReachedTransition(StateMachine stateMachine, IMoveState moveState, float maxSqrDistance, Transform transform) : base(stateMachine)
    {
        _moveState = moveState;
        _maxSqrDistance = maxSqrDistance;
        _transform = transform;
    }

    public override bool IsNeedTransit()
    {
        float sqrDistance = (_transform.position - _moveState.Target.position).sqrMagnitude;

        return sqrDistance <= _maxSqrDistance;
    }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<IdleState>();
        //
    }
}
