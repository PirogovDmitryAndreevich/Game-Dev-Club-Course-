using UnityEngine;

class SeeTargetTransition : Transition
{
    private Enemy _enemy;
    private Transform _transform;

    public SeeTargetTransition(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _enemy = enemy;
        _transform = _enemy.transform;
    }

    public override bool IsNeedTransit() => _enemy.View.TrySeeTarget(out Transform target) 
        && (_transform.position - target.position).sqrMagnitude > _enemy.Attacker.SqrAttackDistance;

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<FollowState>();
    }
}





