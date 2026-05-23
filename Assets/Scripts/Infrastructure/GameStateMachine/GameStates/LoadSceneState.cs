public class LoadSceneState : IPayloadState<SceneID>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IScenesLogicContainer _scenesContainer;
    private readonly IPoolService _poolService;
    private IScene loadedScene;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IScenesLogicContainer scenesContainer,
        IPoolService poolService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _scenesContainer = scenesContainer;
        _poolService = poolService;
    }

    public void Enter(SceneID sceneId)
    {
        _curtain.Show();
        _poolService.Cleanup();
        loadedScene = _scenesContainer.Scenes[sceneId];
        _sceneLoader.Load(sceneId, OnLoaded);
    }

    private void OnLoaded()
    {
        loadedScene.InitGameObjects();
        _gameStateMachine.Enter<GameLoopState>();
    }

    public void Exit()
    {
        _curtain.Hide();        
    }
}
