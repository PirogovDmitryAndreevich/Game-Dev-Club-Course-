class TargetReachedTransition : ReachedTransition
{
    public TargetReachedTransition(StateMachine stateMachine, IMoveState moveState, Enemy enemy) 
        : base(stateMachine, moveState, enemy) { }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<AttackState>();
    }
}





