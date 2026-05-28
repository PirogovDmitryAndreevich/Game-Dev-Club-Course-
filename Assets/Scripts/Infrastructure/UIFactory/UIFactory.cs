using UnityEngine;

public class UIFactory : IUIFactory
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IAssets _assets;
    private readonly IHandlersContainer _handlersContainer;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _save;
    private readonly IStaticData _staticData;

    public UIFactory(GameStateMachine gameStateMachine, IAssets assetProvider, IHandlersContainer handlersContainer,
        IPersistentProgressService progressService, ISaveLoadService save, IStaticData staticData)
    {
        _gameStateMachine = gameStateMachine;
        _assets = assetProvider;
        _handlersContainer = handlersContainer;
        _progressService = progressService;
        _save = save;
        _staticData = staticData;
    }

    public void CreateMainMenu()
    {
        MainMenu menu = _assets.Instantiate(AssetsPath.MainMenuPath)
            .GetComponent<MainMenu>();

        menu.Construct(_gameStateMachine, _progressService, _handlersContainer, _save);
    }

    public void CreateWinWindow(LevelData levelData)
    {
        WinWindow window = _assets.Instantiate(AssetsPath.WinWindowPath)
            .GetComponent<WinWindow>();

        window.Construct(levelData.ID, levelData.NextSceneID, _gameStateMachine, _handlersContainer.Audio);
    }

    public void CreateFailWindow(LevelData levelData, Player player)
    {
        FailWindow window = _assets.Instantiate(AssetsPath.FailWindowPath)
            .GetComponent<FailWindow>();

        window.Construct(levelData.ID, _gameStateMachine, _handlersContainer.Audio, player);
    }

    public Hud CreateHud(bool isDesktop, LevelData levelData, Player player)
    {
        GameObject hudObject = isDesktop
            ? CreateHudDesktop()
            : CreateHudMobile();

        Hud hud = hudObject.GetComponent<Hud>();

        PauseWindow pauseWindow = CreatePauseWindow(levelData.ID);

        hud.Construct(pauseWindow, player);

        hud.HealthBar.Construct(player.Health, player.Defense);
        hud.Inventory.Construct(this);

        return hud;
    }

    public ItemView CreateUIKey(Color color, Transform parent)
    {
        ItemView view = _assets.Instantiate(AssetsPath.ItemViewPath, parent)
            .GetComponent<ItemView>();

        view.Construct(color);

        return view;
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
