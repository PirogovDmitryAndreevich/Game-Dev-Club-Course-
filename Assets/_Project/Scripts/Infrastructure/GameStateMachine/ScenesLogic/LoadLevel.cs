using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LoadLevel : IScene
{
    private readonly SceneID _id;
    private readonly IStaticData _staticData;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IHandlersContainer _handlers;

    public LoadLevel(SceneID id ,IStaticData staticData, IGameFactory gameFactory, IUIFactory uiFactory, IHandlersContainer handlers)
    {
        _id = id;
        _staticData = staticData;
        _gameFactory = gameFactory;
        _uiFactory = uiFactory;
        _handlers = handlers;
    }

    public void InitGameObjects()
    {
        LevelData levelData = _staticData.ForLevel(_id);

        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();
        Player player = _gameFactory.CreatePlayerHero(at: levelData.PlayerInitial.Position, camera);
        BusStop busStop = _gameFactory.CreateBusStop(levelData.BusStop.Position);

        Hud hud = CreateHud(levelData, player);

        List<Enemy> enemies = new List<Enemy>();
        List<Trophy> trophies = new List<Trophy>();

        if (levelData.Locks != null && levelData.Locks.Count > 0)
        {
            foreach (var data in levelData.Locks)
                _gameFactory.CreateLock(data, hud.Inventory);
        }

        if (levelData.MedKits != null && levelData.MedKits.Count > 0)
        {
            foreach (var data in levelData.MedKits)
                _gameFactory.CreateMedKit(at: data.Position, player.Health, data.Value);
        }

        if (levelData.Defenses != null && levelData.Defenses.Count > 0)
        {
            foreach (var data in levelData.Defenses)
                _gameFactory.CreateDefense(at: data.Position, player.Defense, data.Value);
        }

        if (levelData.Trophies != null && levelData.Trophies.Count > 0)
        {
            foreach (var data in levelData.Trophies)
            {
                var trophy = _gameFactory.CreateTrophy(at: data.Position);
                trophies.Add(trophy);
            }
        }

        InitEnemySpawners(levelData, enemies);
        WinWindow winWindow = _uiFactory.CreateWinWindow(levelData);

        InitUI(levelData, player);

        camera.Follow = player.transform;

        Bus bus = _gameFactory.CreateBus(at: levelData.BusStop.BusData.SpawnPosition,
            levelData.BusStop.BusData.EndMovePoint,
            levelData.BusStop.BusData.IsRight);

        InitGameLogic(enemies, trophies, hud, winWindow, busStop, bus, player);
    }

    public void InitGameLogic(List<Enemy> enemies, List<Trophy> trophies, Hud hud, WinWindow winWindow, BusStop busStop
        ,Bus bus, Player player) =>
        new FinishLevelLogic(enemies, trophies, hud.TaskView, winWindow, busStop, bus, player);
    

    private Hud CreateHud(LevelData levelData, Player player) =>
        _uiFactory.CreateHud(YG2.envir.isDesktop, levelData, player);

    private void InitUI(LevelData levelData, Player player)
    {
        _uiFactory.CreateFailWindow(levelData, player);
    }

    private void InitEnemySpawners(LevelData levelData, List<Enemy> enemies)
    {
        AudioListener listener = Camera.main.GetComponent<AudioListener>();
        _handlers.Audio.SetListener(listener);

        List<EnemySpawner> spawners = _gameFactory.CreateEnemySpawners(levelData);

        Enemy enemy;

        foreach (var spawner in spawners)
        {
            enemy = spawner.Spawn();
            enemies.Add(enemy);
        }
    }
}
