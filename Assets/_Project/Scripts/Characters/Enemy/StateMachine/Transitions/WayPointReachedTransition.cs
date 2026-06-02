class WayPointReachedTransition : ReachedTransition
{
    public WayPointReachedTransition(StateMachine stateMachine, IMoveState moveState, float distance, Enemy enemy) :
        base(stateMachine, moveState, distance, enemy)
    { }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<IdleState>();
        //
    }
}





