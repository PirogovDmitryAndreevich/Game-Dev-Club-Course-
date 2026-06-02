using YG;

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
        _sceneLoader.Load(SceneID.Initial, onLoad: OnLoad);
    }

    private void OnLoad() =>
        _gameStateMachine.Enter<LoadProgressState>();

    private void RegisterServices()
    {
        RegisterStaticData();
        _services.RegisterSingle(RegisterInputService());
        _services.RegisterSingle<IAssets>(new AssetProvider());
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _services.RegisterSingle<IScenesLogicContainer>(new ScenesLogicContainer());
        _services.RegisterSingle<IPoolService>(new PoolService());

        _services.RegisterSingle<IPersistentHandlersFactory>(new PersistentHandlersFactory(_services.Single<IAssets>(),
            _services.Single<IPersistentProgressService>()));

        _services.RegisterSingle<IHandlersContainer>(new HandlersContainer(_services.Single<IPersistentHandlersFactory>()));

        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>()));

        _services.RegisterSingle<IGameFactory>(new GameFactory(
            _services.Single<IAssets>(),
            _services.Single<IPersistentProgressService>(),
            _services.Single<ISaveLoadService>(),
            _services.Single<IInputServices>(),
            _services.Single<IHandlersContainer>(),
            _services.Single<IStaticData>(),
            _services.Single<IPoolService>()));

        _services.RegisterSingle<IUIFactory>(new UIFactory(
            _gameStateMachine,
            _services.Single<IAssets>(),
            _services.Single<IHandlersContainer>(),
            _services.Single<IPersistentProgressService>(),
            _services.Single<ISaveLoadService>(),
            _services.Single<IStaticData>()));
    }

    private void RegisterStaticData()
    {
        IStaticData staticData = new StaticDataService();
        staticData.Load();
        _services.RegisterSingle<IStaticData>(staticData);
    }

    public void Exit()
    {
    }

    private IInputServices RegisterInputService()
    {
        if (YG2.envir.isDesktop)
            return new StandaloneInput();
        else
            return new MobileInput();
    }
}
