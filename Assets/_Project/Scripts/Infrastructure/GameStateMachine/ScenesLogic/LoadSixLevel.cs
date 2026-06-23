using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LoadSixLevel : IScene
{
    private const float MinutesCoefficient = 60f;
    private const float LevelTimer = 3f * MinutesCoefficient;
    private const int MaxEnemyOnScene = 6;

    private readonly SceneID _id;
    private readonly IStaticData _staticData;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IHandlersContainer _handlers;
    private readonly ICoroutineRunner _coroutineRunner;

    private BusStop _busStop;
    private Player _player;
    private Bus _bus;
    private FinishWithBusStop _finishWithBusStop;
    private WinWindow _winWindow;
    private Timer _timer;
    private int _enemyOnScene;
    private bool _isPlayerDead;
    private List<Enemy> _enemies;

    public LoadSixLevel(SceneID id, IStaticData staticData, IGameFactory gameFactory,
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
        _enemies = new List<Enemy>();

        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();

        AudioListener listener = Camera.main.GetComponent<AudioListener>();
        _handlers.Audio.SetListener(listener);

        _player = _gameFactory.CreatePlayerHero(at: levelData.PlayerInitial.Position, camera);
        _busStop = _gameFactory.CreateBusStop(levelData.BusStop.Position);

        Hud hud = CreateHud(levelData, _player);
        _timer = _uiFactory.CreateTimer();

        List<EnemySpawner> spawners = _gameFactory.CreateEnemySpawners(levelData);

        _winWindow = _uiFactory.CreateWinWindow(levelData);

        _uiFactory.CreateFailWindow(levelData, _player);

        camera.Follow = _player.transform;

        _bus = _gameFactory.CreateBus(at: levelData.BusStop.BusData.SpawnPosition,
            levelData.BusStop.BusData.EndMovePoint,
            levelData.BusStop.BusData.IsRight);

        _handlers.Audio.PlayMusic(levelData.BGMusic);

        _finishWithBusStop = new FinishWithBusStop(_busStop, _bus, _player);

        _coroutineRunner.StartCoroutine(StartTimer(spawners));

        _player.Health.Died += OnPlayerDied;
    }

    private Hud CreateHud(LevelData levelData, Player player) =>
        _uiFactory.CreateHud(YG2.envir.isDesktop, levelData, player);

    private IEnumerator StartTimer(List<EnemySpawner> spawners)
    {
        _player.Arrow.SetTarget(null);
        float time = LevelTimer;
        _isPlayerDead = false;
        _enemyOnScene = 0;
        _timer.UpdateTimer(time);

        while (time > 0)
        {
            if (_isPlayerDead)
                yield break;

            time -= Time.deltaTime;
            _timer.UpdateTimer(time);

            if (_enemyOnScene < MaxEnemyOnScene)
            {
                _enemyOnScene++;
                SpawnEnemy(spawners);
            }

            yield return null;
        }

        while (_enemies.Count > 0)
        {
            if (_isPlayerDead)
                yield break;

            yield return null;
        }

        Reached();
    }

    private void OnPlayerDied()
    {
        _player.Health.Died -= OnPlayerDied;
        _isPlayerDead = true;
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

    private void Reached()
    {
        _timer.gameObject.SetActive(false);
        _busStop.EnableInteract();
        _player.Arrow.SetTarget(_busStop.transform);
        _busStop.Interacted += StartCutscene;
    }

    private void StartCutscene()
    {
        _player.Arrow.SetTarget(null);

        _busStop.Interacted -= StartCutscene;
        _finishWithBusStop.CutsceneEnded += ShowWinWindow;
        _finishWithBusStop.Play();
    }

    private void ShowWinWindow()
    {
        _finishWithBusStop.CutsceneEnded -= ShowWinWindow;

        _winWindow.gameObject.SetActive(true);
        _winWindow.Show();
    }
}
