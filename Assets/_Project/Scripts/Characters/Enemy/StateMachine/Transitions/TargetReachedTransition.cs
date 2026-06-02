class TargetReachedTransition : ReachedTransition
{
    public TargetReachedTransition(StateMachine stateMachine, IMoveState moveState, float distance, Enemy enemy) 
        : base(stateMachine, moveState, distance, enemy) { }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<AttackState>();
    }
}





