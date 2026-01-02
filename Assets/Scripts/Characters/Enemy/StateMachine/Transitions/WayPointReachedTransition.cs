using UnityEditor;
using UnityEngine;

class WayPointReachedTransition : ReachedTransition
{
    public WayPointReachedTransition(StateMachine stateMachine, IMoveState moveState, float maxSqrDistance, Transform transform) :
        base(stateMachine, moveState, maxSqrDistance, transform) { }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<IdleState>();
        //
    }
}





