using Cinemachine;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private readonly IAssets _assets;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _save;
    private readonly IInputServices _input;
    private readonly IHandlersContainer _handlers;
    private readonly IStaticData _staticData;

    public GameFactory(IAssets assets, IPersistentProgressService progressService, ISaveLoadService save, IInputServices input,
        IHandlersContainer handlers, IStaticData staticData)
    {
        _assets = assets;
        _progressService = progressService;
        _save = save;
        _input = input;
        _handlers = handlers;
        _staticData = staticData;
    }

    public CinemachineVirtualCamera CreateVirtualCamera()
    {
        CinemachineVirtualCamera camera = _assets.Instantiate(AssetsPath.VirtualCameraPath)
            .GetComponent<CinemachineVirtualCamera>();

        return camera;
    }

    public Player CreatePlayerHero(Vector2 at, CinemachineVirtualCamera camera)
    {
        Player player = _assets.Instantiate(AssetsPath.PlayerPath, at: at)
            .GetComponent<Player>();

        player.Construct(_progressService, _save, _input);
        player.PlayerSounds.Construct(_handlers.Audio);
        player.CameraShake.Construct(camera);
        player.FX.Construct(player.Health, player.Defense);

        return player;
    }    

    public Enemy CreateEnemy(EnemyTypeId id,Vector2 at, WayPoint[] wayPoints)
    {
        EnemyStaticData data = _staticData.ForEnemy(enemyTypeId: id);
        Enemy enemy = Object.Instantiate(data.EnemyPrefab);
        enemy.Construct(data);
        enemy.Sound.Construct(_handlers.Audio);

        return null;
    }
}