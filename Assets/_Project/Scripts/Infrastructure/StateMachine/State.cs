abstract class State
{
    protected Transition[] Transitions;

    protected State(StateMachine stateMachine) { }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    public virtual void TryTransit()
    {
        foreach (Transition transition in Transitions)
        {
            if (transition.IsNeedTransit())
            {
                transition.Transit();
                return;
            }
        }
    }
}





