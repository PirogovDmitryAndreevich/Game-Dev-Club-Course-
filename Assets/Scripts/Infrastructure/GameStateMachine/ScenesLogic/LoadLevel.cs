using Cinemachine;
using UnityEngine;

public class LoadLevel : IScene
{
    private const string PlayerInitialPointTag = "PlayerInitialPoint";
    private readonly GameStateMachine _gameStateMachine;
    private readonly IGameFactory _gameFactory;

    public LoadLevel(GameStateMachine gameStateMachine, IGameFactory gameFactory)
    {
        _gameStateMachine = gameStateMachine;
        _gameFactory = gameFactory;
    }

    public void InitGameObjects()
    {
        CinemachineVirtualCamera camera = _gameFactory.CreateVirtualCamera();
        Player player = _gameFactory.CreatePlayerHero(at: GameObject.FindWithTag(PlayerInitialPointTag).transform.position, camera);

        camera.Follow = player.transform;
    }
}
