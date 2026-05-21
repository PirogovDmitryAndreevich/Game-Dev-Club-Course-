using UnityEngine;

class ReachedTransition : Transition
{
    private Enemy _enemy;
    private IMoveState _moveState;
    private Transform _transform;

    public ReachedTransition(StateMachine stateMachine, IMoveState moveState, Enemy enemy) : base(stateMachine)
    {
        _enemy = enemy; 
        _moveState = moveState;
        _transform = _enemy.transform;
    }

    public override bool IsNeedTransit()
    {
        float sqrDistance = (_transform.position - _moveState.Target.position).sqrMagnitude;

        return sqrDistance <= _enemy.StaticData.MaxSqrDistance;
    }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<IdleState>();
        //
    }
}
