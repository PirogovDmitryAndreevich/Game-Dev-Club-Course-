class EndIdleTransition : Transition
{
    private IdleState _idleState;

    public EndIdleTransition(StateMachine stateMachine, IdleState idleState) : base(stateMachine) => _idleState = idleState;

    public override bool IsNeedTransit() => _idleState.isEndWait;

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChangeState<PatrolState>();
    }
}





