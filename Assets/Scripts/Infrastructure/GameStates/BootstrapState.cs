public class BootstrapState : IState
{
    private const string InitialScene = "Initial";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;

        RegisterServices();
    }

    public void Enter()
    {        
        _sceneLoader.Load(InitialScene, onLoad: EnterLoadLevel);
    }

    private void EnterLoadLevel()
    {
        _gameStateMachine.Enter<LoadSceneState, string>("MainMenu");
    }

    private void RegisterServices()
    {
        Game.InputServices = RegisterInputService();
    }

    public void Exit()
    {
    }

    private IInputServices RegisterInputService() =>
        new StandaloneInput();
}
