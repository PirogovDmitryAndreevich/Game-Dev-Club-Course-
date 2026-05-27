using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoadLevel : IScene
{
    private readonly IStaticData _staticData;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IHandlersContainer _handlers;

    public LoadLevel(IStaticData staticData, IGameFactory gameFactory, IUIFactory uiFactory, IHandlersContainer handlers)
    {
        _staticData = staticData;
        _gameFactory = gameFactory;
        _uiFactory = uiFactory;
        _handlers = handlers;
    }

    public void InitGameObjects()
    {
        string sceneKey = SceneManager.GetActiveScene().name;
        LevelData levelData = _staticData.ForLevel(sceneKey);

        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();
        Player player = _gameFactory.CreatePlayerHero(at: levelData.PlayerInitial.Position, camera);

        Hud hud = CreateHud(levelData, player);

        if (levelData.Locks != null && levelData.Locks.Count > 0)
        {
            foreach(var data in levelData.Locks)
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
                _gameFactory.CreateTrophy(at: data.Position);
        }

        InitEnemySpawners(levelData);
        InitUI(levelData, player);

        camera.Follow = player.transform;
    }

    private Hud CreateHud(LevelData levelData, Player player) =>
        _uiFactory.CreateHud(YG2.envir.isDesktop, levelData, player);

    private void InitUI(LevelData levelData, Player player)
    {
        
        _uiFactory.CreateWinWindow(levelData);
        _uiFactory.CreateFailWindow(levelData, player);
    }

    private void InitEnemySpawners(LevelData levelData)
    {
        AudioListener listener = Camera.main.GetComponent<AudioListener>();
        _handlers.Audio.SetListener(listener);

        List<EnemySpawner> spawners = _gameFactory.CreateEnemySpawners(levelData);

        foreach (var spawner in spawners)
            spawner.Spawn();
    }
}
