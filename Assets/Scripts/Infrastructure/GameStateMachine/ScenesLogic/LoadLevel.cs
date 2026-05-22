using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : IScene
{
    private const string PlayerInitialPointTag = "PlayerInitialPoint";
    private readonly GameStateMachine _gameStateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly IHandlersContainer _handlers;

    public LoadLevel(GameStateMachine gameStateMachine, IGameFactory gameFactory, IHandlersContainer handlers)
    {
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _handlers = handlers;
    }

    public void InitGameObjects()
    {
        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();
        Player player = _gameFactory.CreatePlayerHero(at: GameObject.FindWithTag(PlayerInitialPointTag).transform.position, camera);

        InitEnemySpawners();

        camera.Follow = player.transform;
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
