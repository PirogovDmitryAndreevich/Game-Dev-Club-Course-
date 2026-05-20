internal class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _persistentProgress;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgress, ISaveLoadService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _persistentProgress = persistentProgress;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<PersistentHandlersCreateState>();
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew() =>
        _persistentProgress.Progress =
            _saveLoadService.
            LoadProgress()
            ?? NewProgress();

    private SaveData NewProgress() =>
        new SaveData();    
}