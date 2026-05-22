using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private const string EnemySpawnerParentName = "EnemySpawners";

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
        PlayerStaticData data = _staticData.ForPlayer();

        Player player = _assets.Instantiate(AssetsPath.PlayerPath, at: at)
            .GetComponent<Player>();

        player.Construct(_progressService, _save, _input);
        player.PlayerSounds.Construct(_handlers.Audio);
        player.CameraShake.Construct(camera);
        player.FX.Construct(player.Health, player.Defense);
        player.Attacker.Construct(data);

        return player;
    }

    public Enemy CreateEnemy(EnemyTypeId id, Vector2 at, List<WayPoint> wayPoints)
    {
        EnemyStaticData data = _staticData.ForEnemy(enemyTypeId: id);
        Enemy enemy = Object.Instantiate(data.EnemyPrefab, position: at, Quaternion.identity);
        enemy.Construct(data , wayPoints);
        enemy.Sound.Construct(_handlers.Audio);
        enemy.Attacker.Construct(data);

        return null;
    }

    public List<EnemySpawner> CreateEnemySpawners(string sceneKey)
    {
        LevelData levelData = _staticData.ForLevel(sceneKey);

        Transform parent = new GameObject(EnemySpawnerParentName).transform;

        List<EnemySpawner> spawners = new List<EnemySpawner>();

        foreach (EnemySpawnerData spawnerData in levelData.EnemySpawnerDatas)
        {
            EnemySpawner spawner = CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.TypeId, spawnerData.WayPoints, parent);
            spawners.Add(spawner);
        }

        return spawners;
    }

    private EnemySpawner CreateSpawner(Vector2 position, string id, EnemyTypeId typeId, List<WayPointData> wayPointsData, Transform parent)
    {
        EnemySpawner spawner = _assets.Instantiate(AssetsPath.SpawnerPath, position)
            .GetComponent<EnemySpawner>();

        List<WayPoint> wayPointsList = new List<WayPoint>();

        foreach (WayPointData point in wayPointsData)
        {
            WayPoint wayPoint = _assets.Instantiate(AssetsPath.WayPoinPath, point.Position)
                .GetComponent<WayPoint>();

            wayPoint.transform.SetParent(spawner.transform);
            wayPointsList.Add(wayPoint);
        }

        spawner.transform.SetParent(parent);
        spawner.Construct(this, typeId, wayPointsList);

        return spawner;
    }
}