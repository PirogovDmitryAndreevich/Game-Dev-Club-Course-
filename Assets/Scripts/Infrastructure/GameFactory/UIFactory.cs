using UnityEngine;

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

    public void CreateHud(bool isDesktop, SceneID currentScene, Player player)
    {
        GameObject hudObject = isDesktop
            ? CreateHudDesktop()
            : CreateHudMobile();

        Hud _hud = hudObject.GetComponent<Hud>();

        PauseWindow pauseWindow = CreatePauseWindow(currentScene);
        
        _hud.Construct(pauseWindow);

        _hud.HealthBar.Construct(player.Health, player.Defense);
    }

    private PauseWindow CreatePauseWindow(SceneID currentScene)
    {
        PauseWindow window = _assets.Instantiate(AssetsPath.PauseWindowPath)
            .GetComponent<PauseWindow>();

        window.Construct(currentScene, _handlersContainer.Audio, _gameStateMachine);
        window.AudioSettings.Construct(_progressService, _save);

        return window;
    }

    private GameObject CreateHudMobile() =>
       _assets.Instantiate(AssetsPath.HudMobilePath);

    private GameObject CreateHudDesktop() =>
       _assets.Instantiate(AssetsPath.HudDesktopPath);
}
