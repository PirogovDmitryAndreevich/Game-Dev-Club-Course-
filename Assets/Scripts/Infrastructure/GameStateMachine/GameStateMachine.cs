using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader, AllServices services)
    {
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),

            [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),
            services.Single<ISaveLoadService>()),

            [typeof(PersistentHandlersCreateState)] = new PersistentHandlersCreateState(this, services.Single<IHandlersContainer>()),

            [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, services.Single<IUIFactory>()),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _currentState?.Exit();

        TState state = GetState<TState>();
        _currentState = state;
        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
        _states[typeof(TState)] as TState;
}
