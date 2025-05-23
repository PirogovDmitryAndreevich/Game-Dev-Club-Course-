using UnityEngine;

class TargetReachedTransition : Transition
{
    private float _maxSqrDistance = 14.7f;
    private PatrolState _patrolState;
    private Transform _transform;

    public TargetReachedTransition(StateMachine stateMachine, PatrolState patrolState, float maxSqrDistance, Transform transform) : base(stateMachine)
    {
        _patrolState = patrolState;
        _maxSqrDistance = maxSqrDistance;
        _transform = transform;
    }

    public override bool IsNeedTransit()
    {
        bool isTargetReached;
        float sqrDistance = (_transform.position - _patrolState.Target.position).sqrMagnitude;
        isTargetReached = sqrDistance < _maxSqrDistance;

        return isTargetReached;
    }

    public override void Transit() 
    {
        base.Transit();
        StateMachine.ChangeState<IdleState>();
        //
    } 
} 





