using System.Collections.Generic;

public class FinishLevelLogic
{
    private BusStop _busStop;
    private TasksView _taskView;
    private WinWindow _winWindow;
    private int _diedEnemies;
    private int _taskEnemies;
    private int _trophiesCollected;
    private int _allTrophies;

    private List<Enemy> _enemies;
    private List<Trophy> _trophies;
    private FinishWithBusStop _finishWithBusStop;

    public FinishLevelLogic(List<Enemy> enemies, List<Trophy> trophies, TasksView taskView, WinWindow winWindow, BusStop busStop, Bus bus, Player player)
    {
        _busStop = busStop;
        _taskView = taskView;
        _winWindow = winWindow;
        _enemies = enemies;
        _trophies = trophies;

        _trophiesCollected = 0;
        _allTrophies = trophies.Count;

        _taskEnemies = _enemies.Count;
        _diedEnemies = _taskEnemies - _enemies.Count;

        foreach (Enemy enemy in enemies)
            enemy.EnemyDied += EnemyDied;

        foreach (Trophy trophy in trophies)
            trophy.Collected += TrophyCollected;

         _finishWithBusStop = new FinishWithBusStop(busStop, bus, player);
        bus.gameObject.SetActive(false);

        _finishWithBusStop.CutsceneEnded += ShowWinWindow;

        UpdateCounter();
    }

    private void TrophyCollected(Trophy trophy)
    {
        trophy.Collected -= TrophyCollected;
        _trophies.Remove(trophy);

        _trophiesCollected++;
    }

    private void EnemyDied(Enemy enemy)
    {
        enemy.EnemyDied -= EnemyDied;
        _enemies.Remove(enemy);

        _diedEnemies = _taskEnemies - _enemies.Count;
        UpdateCounter();

        if (_diedEnemies == _taskEnemies)
            Reached();
    }

    private void UpdateCounter() =>
        _taskView.UpdateCounter(_diedEnemies, _taskEnemies);

    private void Reached()
    {
        _taskView.Reached();
        _busStop.EnableInteract();
        _busStop.Interacted += StartCutscene;
    }

    private void StartCutscene()
    {
        _busStop.Interacted -= StartCutscene;
        _finishWithBusStop.Play();
    }

    private void ShowWinWindow()
    {
        _finishWithBusStop.CutsceneEnded -= ShowWinWindow;        

        _winWindow.gameObject.SetActive(true);
        _winWindow.Initialize(_trophiesCollected, _allTrophies);
        _winWindow.Show();
    }
}
