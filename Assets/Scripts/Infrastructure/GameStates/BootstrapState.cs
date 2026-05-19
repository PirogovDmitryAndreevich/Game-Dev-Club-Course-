public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _services = services;
        RegisterServices();
    }

    public void Enter()
    {        
        _sceneLoader.Load(SceneID.Initial, onLoad: EnterLoadLevel);
    }

    private void EnterLoadLevel()
    {
        _gameStateMachine.Enter<LoadSceneState, SceneID>(SceneID.MainMenu);
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IInputServices>(RegisterInputService());
        _services.RegisterSingle<IAssets>(new AssetProvider());
        _services.RegisterSingle<IUIFactory>(new UIFactory(_gameStateMachine, AllServices.Container.Single<IAssets>()));
    }

    public void Exit()
    {
    }

    private IInputServices RegisterInputService() =>
        new StandaloneInput();
}
