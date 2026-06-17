using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LoadArenaLogic : IScene
{
    private const int MaxEnemyOnScene = 8;

    private readonly SceneID _id;
    private readonly IStaticData _staticData;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IHandlersContainer _handlers;
    private readonly ICoroutineRunner _coroutineRunner;

    private Player _player;
    private Timer _timer;
    private ArenaFinishWindow _arenaFinishWindow;
    private List<Enemy> _enemies = new List<Enemy>();
    private int _enemyOnScene = 0;
    private float _time;
    private bool _isPlayerDead;

    public LoadArenaLogic(SceneID id, IStaticData staticData, IGameFactory gameFactory,
        IUIFactory uiFactory, IHandlersContainer handlers, ICoroutineRunner coroutineRunner)
    {
        _id = id;
        _staticData = staticData;
        _gameFactory = gameFactory;
        _uiFactory = uiFactory;
        _handlers = handlers;
        _coroutineRunner = coroutineRunner;
    }

    public void InitGameObjects()
    {
        LevelData levelData = _staticData.ForLevel(_id);
        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();
        AudioListener listener = Camera.main.GetComponent<AudioListener>();
        _handlers.Audio.SetListener(listener);

        _player = _gameFactory.CreatePlayerHero(at: levelData.PlayerInitial.Position, camera);
        camera.Follow = _player.transform;

        Hud hud = CreateHud(levelData, _player);
        _timer = _uiFactory.CreateTimer();

        _arenaFinishWindow = _uiFactory.CreateArenaFinishWindow();

        List<EnemySpawner> spawners = _gameFactory.CreateEnemySpawners(levelData);

        _handlers.Audio.PlayMusic(levelData.BGMusic);

        _coroutineRunner.StartCoroutine(StartTimer(spawners));

        _player.Health.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _player.Health.Died -= OnPlayerDied;
        _isPlayerDead = true;

        _arenaFinishWindow.Initialize(_time);
        _arenaFinishWindow.Show();
    }

    private IEnumerator StartTimer(List<EnemySpawner> spawners)
    {
        _player.Arrow.SetTarget(null);
        _time = 0f;
        _enemyOnScene = 0;
        _timer.UpdateTimer(_time);

        while (!_isPlayerDead)
        {
            _time += Time.deltaTime;
            _timer.UpdateTimer(_time);

            if (_enemyOnScene < MaxEnemyOnScene)
            {
                _enemyOnScene++;
                SpawnEnemy(spawners);
            }

            yield return null;
        }
    }

    private void SpawnEnemy(List<EnemySpawner> spawners)
    {
        EnemySpawner spawner = spawners[Random.Range(0, spawners.Count)];
        var enemy = spawner.Spawn();
        _enemies.Add(enemy);

        enemy.EnemyDied += EnemyDied;
    }

    private void EnemyDied(Enemy enemy)
    {
        enemy.EnemyDied -= EnemyDied;
        _enemies.Remove(enemy);
        _enemyOnScene--;
    }

    private Hud CreateHud(LevelData levelData, Player player) =>
        _uiFactory.CreateHud(YG2.envir.isDesktop, levelData, player);
}