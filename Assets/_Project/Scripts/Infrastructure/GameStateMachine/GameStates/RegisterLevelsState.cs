using System;

public class RegisterLevelsState : IState
{
    private GameStateMachine _gameStateMachine;
    private AllServices _services;
    private readonly ICoroutineRunner _coroutineRunner;
    private IScenesLogicContainer _scenesContainer;

    public RegisterLevelsState(GameStateMachine gameStateMachine, AllServices services, ICoroutineRunner coroutineRunner)
    {
        _gameStateMachine = gameStateMachine;
        _services = services;
        _coroutineRunner = coroutineRunner;
        _scenesContainer = _services.Single<IScenesLogicContainer>();
    }

    public void Enter()
    {
        _scenesContainer.AddNewScene(SceneID.MainMenu, new MainMenuScene(_services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_1, new LoadLevel(SceneID.Level_1, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_2, new LoadLevel(SceneID.Level_2, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_3, new LoadLevel(SceneID.Level_3, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_4, new LoadLevel(SceneID.Level_4, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_5, new LoadLevel(SceneID.Level_5, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_6, new LoadSixLevel(SceneID.Level_6, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>(), _coroutineRunner));
        _scenesContainer.AddNewScene(SceneID.Level_7, new LoadLevel(SceneID.Level_7, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_8, new LoadLevel(SceneID.Level_8, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));

        _gameStateMachine.Enter<LoadSceneState, SceneID>(SceneID.MainMenu);
    }

    public void Exit()
    {

    }
}