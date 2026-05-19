using System;
using System.Collections.Generic;

abstract class StateMachine 
{
    protected State CurrentState;
    protected Dictionary<Type, State> States;

    public void Update()
    {
        if (CurrentState == null)
            return;

        CurrentState.Update();

        CurrentState.TryTransit();
    }

    public void ChangeState<TState>() where TState : State
    {
        if (CurrentState != null && CurrentState.GetType() == typeof(TState))
            return;

        if (States.TryGetValue(typeof(TState), out State newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}





