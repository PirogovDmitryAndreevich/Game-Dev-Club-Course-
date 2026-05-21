using UnityEngine;

public class MainMenuScene : IScene
{
    private IUIFactory _uiFactory;

    public MainMenuScene(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public void InitGameObjects()
    {
        _uiFactory.CreateMainMenu();
    }
}
