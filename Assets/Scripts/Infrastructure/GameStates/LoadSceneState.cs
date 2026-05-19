using System.Collections.Generic;

public class LoadSceneState : IPayloadState<SceneID>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private Dictionary<SceneID, IScene> _scenes;
    private IScene loadedScene;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUIFactory uiFactory)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;

        _scenes = new Dictionary<SceneID, IScene>
        {
            [SceneID.MainMenu] = new MainMenuScene(uiFactory),

        };
    }

    public void Enter(SceneID sceneId)
    {
        loadedScene = _scenes[sceneId];
        _sceneLoader.Load(sceneId, OnLoaded);
    }

    private void OnLoaded() => 
        loadedScene.InitGameObjects();

    public void Exit()
    {
    }
}

public class LoadLevel : IScene
{
    private readonly GameStateMachine _gameStateMachine;

    public LoadLevel(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void InitGameObjects()
    {
        throw new System.NotImplementedException();
    }
}
