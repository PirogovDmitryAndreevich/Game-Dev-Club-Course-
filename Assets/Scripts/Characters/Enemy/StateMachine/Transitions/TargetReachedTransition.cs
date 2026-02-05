using UnityEngine;

class TargetReachedTransition : ReachedTransition
{
    public TargetReachedTransition(StateMachine stateMachine, IMoveState moveState, float maxSqrDistance,
        Transform transform) : base(stateMachine, moveState, maxSqrDistance, transform) { }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<AttackState>();
    }
}





