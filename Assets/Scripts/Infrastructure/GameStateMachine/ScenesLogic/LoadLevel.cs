using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoadLevel : IScene
{
    private const string PlayerInitialPointTag = "PlayerInitialPoint";
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

        InitEnemySpawners(levelData);
        InitUI(levelData, player);

        camera.Follow = player.transform;
    }

    private void InitUI(LevelData levelData, Player player)
    {
        _uiFactory.CreateHud(YG2.envir.isDesktop, levelData, player);
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
