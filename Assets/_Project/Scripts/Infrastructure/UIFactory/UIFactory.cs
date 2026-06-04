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

        menu.Construct(_progressService, _handlersContainer);
        menu.Settings.Construct(_handlersContainer.Audio);
        menu.Settings.AudioSlider.Construct(_progressService, _save);
        menu.LevelSelector.StartButtonSound.Construct(_handlersContainer.Audio);
        menu.LevelSelector.ReturnButtonSound.Construct(_handlersContainer.Audio);
        menu.LevelSelector.Construct(_gameStateMachine, _handlersContainer.Audio, _staticData, this);
        menu.LeaderBoard.Construct(_progressService);
        menu.ShopWindow.Construct(_handlersContainer.Audio);
    }

    public WinWindow CreateWinWindow(LevelData levelData)
    {
        WinWindow window = _assets.Instantiate(AssetsPath.WinWindowPath)
            .GetComponent<WinWindow>();

        window.Construct(levelData.ID, levelData.NextSceneID, _gameStateMachine, _handlersContainer.Audio,
            _progressService, _save, levelData);
        window.TrophyCounter.Construct(levelData);

        return window;
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
        hud.StatusBar.Construct(_progressService);

        return hud;
    }

    public ItemView CreateUIKey(Color color, Transform parent)
    {
        ItemView view = _assets.Instantiate(AssetsPath.ItemViewPath, parent)
            .GetComponent<ItemView>();

        view.Construct(color);

        return view;
    }

    public LevelCard CreateLevelCard(LevelData data, Transform parent)
    {
        LevelCard card = _assets.Instantiate(AssetsPath.LevelCardPath, parent)
            .GetComponent<LevelCard>();

        card.Construct(data.Name, data.Sprite, data.ID, _progressService);
        card.Sound.Construct(_handlersContainer.Audio);

        return card;
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
