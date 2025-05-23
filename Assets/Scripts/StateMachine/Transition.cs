using System;

abstract class Transition
{
    protected StateMachine StateMachine;

    public event Action Transiting;

    protected Transition(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public abstract bool IsNeedTransit();

    public virtual void Transit()
    {
        Transiting?.Invoke();
    }
}





