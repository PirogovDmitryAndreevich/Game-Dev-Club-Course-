internal class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _persistentProgress;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IStaticData _staticData;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgress,
        ISaveLoadService saveLoadService, IStaticData staticData)
    {
        _gameStateMachine = gameStateMachine;
        _persistentProgress = persistentProgress;
        _saveLoadService = saveLoadService;
        _staticData = staticData;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<PersistentHandlersCreateState>();
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
        _persistentProgress.Progress =
            _saveLoadService.
            LoadProgress()
            ?? NewProgress();

        LevelData levelData = _staticData.ForLevel(SceneID.Level_1);
        _persistentProgress.Progress.LevelsProgress.OpenNewLevel(levelData.ID, levelData.Trophies.Count);
    }

    private SaveData NewProgress() =>
        new SaveData();    
}