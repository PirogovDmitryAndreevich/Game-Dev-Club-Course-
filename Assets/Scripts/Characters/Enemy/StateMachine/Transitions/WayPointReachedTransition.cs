class WayPointReachedTransition : ReachedTransition
{
    public WayPointReachedTransition(StateMachine stateMachine, IMoveState moveState, Enemy enemy) :
        base(stateMachine, moveState, enemy) { }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<IdleState>();
        //
    }
}





