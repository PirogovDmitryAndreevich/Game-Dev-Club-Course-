using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LoadLevel : IScene
{
    private const string PlayerInitialPointTag = "PlayerInitialPoint";
    private readonly SceneID _id;
    private readonly GameStateMachine _gameStateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IHandlersContainer _handlers;

    public LoadLevel(SceneID id, GameStateMachine gameStateMachine, IGameFactory gameFactory, IUIFactory uiFactory, IHandlersContainer handlers)
    {
        _id = id;
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _uiFactory = uiFactory;
        _handlers = handlers;
    }

    public void InitGameObjects()
    {
        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();
        Player player = _gameFactory.CreatePlayerHero(at: GameObject.FindWithTag(PlayerInitialPointTag).transform.position, camera);

        InitEnemySpawners();
        _uiFactory.CreateHud(YG2.envir.isDesktop, _id, player);
        InitUI();

        camera.Follow = player.transform;
    }

    private void InitUI()
    {
        
    }

    private void InitEnemySpawners()
    {
        AudioListener listener = Camera.main.GetComponent<AudioListener>();
        _handlers.Audio.SetListener(listener);
        string sceneKey = SceneManager.GetActiveScene().name;
        List<EnemySpawner> spawners = _gameFactory.CreateEnemySpawners(sceneKey);

        foreach (var spawner in spawners)
            spawner.Spawn();
    }
}
