using System;

public class RegisterLevelsState : IState
{
    private GameStateMachine _gameStateMachine;
    private AllServices _services;
    private IScenesLogicContainer _scenesContainer;

    public RegisterLevelsState(GameStateMachine gameStateMachine, AllServices services)
    {
        _gameStateMachine = gameStateMachine;
        _services = services;
        _scenesContainer = _services.Single<IScenesLogicContainer>();
    }

    public void Enter()
    {
        _scenesContainer.AddNewScene(SceneID.MainMenu, new MainMenuScene(_services.Single<IUIFactory>()));
        _scenesContainer.AddNewScene(SceneID.Level_1, new LoadLevel(SceneID.Level_1, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_2, new LoadLevel(SceneID.Level_2, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));
        _scenesContainer.AddNewScene(SceneID.Level_3, new LoadLevel(SceneID.Level_3, _services.Single<IStaticData>(), _services.Single<IGameFactory>(),
             _services.Single<IUIFactory>(), _services.Single<IHandlersContainer>()));

        _gameStateMachine.Enter<LoadSceneState, SceneID>(SceneID.MainMenu);
    }

    public void Exit()
    {

    }
}