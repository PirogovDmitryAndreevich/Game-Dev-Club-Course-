public class PersistentHandlersCreateState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IHandlersContainer _handlersContainer;

    public PersistentHandlersCreateState(GameStateMachine gameStateMachine, IHandlersContainer handlersContainer)
    {
        _gameStateMachine = gameStateMachine;
        _handlersContainer = handlersContainer;
    }

    public void Enter()
    {
        _handlersContainer.CreateHandlers();
        _gameStateMachine.Enter<LoadSceneState, SceneID>(SceneID.MainMenu);
    }

    public void Exit()
    {
    }
}