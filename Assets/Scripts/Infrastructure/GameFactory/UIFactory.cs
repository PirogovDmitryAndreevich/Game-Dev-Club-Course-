public class UIFactory : IUIFactory
{    
    private readonly GameStateMachine _gameStateMachine;
    private readonly IAssets _assets;
    private readonly IHandlersContainer _handlersContainer;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _save;

    public UIFactory(GameStateMachine gameStateMachine, IAssets assetProvider, IHandlersContainer handlersContainer,
        IPersistentProgressService progressService, ISaveLoadService save)
    {
        _gameStateMachine = gameStateMachine;
        _assets = assetProvider;
        _handlersContainer = handlersContainer;
        _progressService = progressService;
        _save = save;
    }

    public void CreateMainMenu()
    {
        MainMenu menu = _assets.Instantiate(AssetsPath.MainMenuPath)
            .GetComponent<MainMenu>();

        menu.Construct(_gameStateMachine, _progressService, _handlersContainer, _save);
    }
}
