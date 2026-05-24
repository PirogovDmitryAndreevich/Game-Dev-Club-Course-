using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoadLevel : IScene
{
    private const string PlayerInitialPointTag = "PlayerInitialPoint";
    private readonly SceneID _id;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IHandlersContainer _handlers;

    public LoadLevel(SceneID id, IGameFactory gameFactory, IUIFactory uiFactory, IHandlersContainer handlers)
    {
        _id = id;
        _gameFactory = gameFactory;
        _uiFactory = uiFactory;
        _handlers = handlers;
    }

    public void InitGameObjects()
    {
        string sceneKey = SceneManager.GetActiveScene().name;

        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();
        Player player = _gameFactory.CreatePlayerHero(at: GameObject.FindWithTag(PlayerInitialPointTag).transform.position, camera);

        InitEnemySpawners(sceneKey);
        InitUI(sceneKey, player);

        camera.Follow = player.transform;
    }

    private void InitUI(string sceneKey, Player player)
    {
        _uiFactory.CreateHud(YG2.envir.isDesktop, sceneKey, player);
        _uiFactory.CreateFailWindow(sceneKey, player);
    }

    private void InitEnemySpawners(string sceneKey)
    {
        AudioListener listener = Camera.main.GetComponent<AudioListener>();
        _handlers.Audio.SetListener(listener);
        
        List<EnemySpawner> spawners = _gameFactory.CreateEnemySpawners(sceneKey);

        foreach (var spawner in spawners)
            spawner.Spawn();
    }
}
