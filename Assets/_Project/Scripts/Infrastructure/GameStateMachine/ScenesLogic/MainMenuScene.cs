using UnityEngine;

public class MainMenuScene : IScene
{
    private IUIFactory _uiFactory;
    private readonly IHandlersContainer _handlers;

    public MainMenuScene(IUIFactory uiFactory, IHandlersContainer handlers)
    {
        _uiFactory = uiFactory;
        _handlers = handlers;
    }

    public void InitGameObjects()
    {
        _uiFactory.CreateMainMenu();
        _handlers.Audio.PlayMainMenuMusic();
    }
}
