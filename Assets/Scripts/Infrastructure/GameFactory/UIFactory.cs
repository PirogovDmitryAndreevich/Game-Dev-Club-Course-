using UnityEngine;

public interface IUIFactory : IService
{
    void CreateMainMenu();
}

public class UIFactory : IUIFactory
{    
    private readonly GameStateMachine _gameStateMachine;
    private readonly IAssets _assets;

    public UIFactory(GameStateMachine gameStateMachine, IAssets assetProvider)
    {
        _gameStateMachine = gameStateMachine;
        _assets = assetProvider;
    }

    public void CreateMainMenu()
    {
        MainMenu menu = _assets.Instantiate(AssetsPath.MainMenuPath)
            .GetComponent<MainMenu>();

        menu.Construct(_gameStateMachine);
    }
}
