using UnityEngine;

class ReachedTransition : Transition
{
    private Enemy _enemy;
    private IMoveState _moveState;
    private Transform _transform;
    private float _distance;

    public ReachedTransition(StateMachine stateMachine, IMoveState moveState, float distance ,Enemy enemy) : base(stateMachine)
    {
        _enemy = enemy; 
        _moveState = moveState;
        _transform = _enemy.transform;
        _distance = distance;
    }

    public override bool IsNeedTransit()
    {
        float sqrDistance = (_transform.position - _moveState.Target.position).sqrMagnitude;

        return sqrDistance <= _distance;
    }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<IdleState>();
        //
    }
}
