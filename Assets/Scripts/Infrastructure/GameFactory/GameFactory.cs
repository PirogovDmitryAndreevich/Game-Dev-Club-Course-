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
    private readonly IPoolService _poolService;

    public GameFactory(IAssets assets, IPersistentProgressService progressService, ISaveLoadService save, IInputServices input,
        IHandlersContainer handlers, IStaticData staticData, IPoolService poolService)
    {
        _assets = assets;
        _progressService = progressService;
        _save = save;
        _input = input;
        _handlers = handlers;
        _staticData = staticData;
        _poolService = poolService;
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

    public BusStop CreateBusStop(Vector2 at)
    {
        BusStop busStop = _assets.Instantiate(AssetsPath.BusStopPath, at)
            .GetComponent<BusStop>();

        busStop.Construct(_handlers.Audio);

        return busStop;
    }

    public Lock CreateLock(LockData data, Inventory inventory)
    {
        Lock lockBarrier = _assets.Instantiate(AssetsPath.LockPath, data.Position)
            .GetComponent<Lock>();

        Key key = CreateKey(data.Key, data.Color, inventory);

        lockBarrier.Construct(key, data.Color, inventory);

        return lockBarrier;
    }

    public Trophy CreateTrophy(Vector2 at)
    {
        var trophy = _assets.Instantiate(AssetsPath.TrophyPath, at: at)
            .GetComponent<Trophy>();

        trophy.Construct(_handlers.Audio);

        return trophy;
    }

    public MedKit CreateMedKit(Vector2 at, PlayerHealth health, int value)
    {
        var medKit = _assets.Instantiate(AssetsPath.MedKitPath, at: at)
            .GetComponent<MedKit>();

        medKit.Construct(_handlers.Audio, health, value);

        return medKit;
    }

    public Defense CreateDefense(Vector2 at, PlayerDefense playerDefense, int value)
    {
        var defense = _assets.Instantiate(AssetsPath.DefensePath, at: at)
            .GetComponent<Defense>();

        defense.Construct(_handlers.Audio, playerDefense, value);

        return defense;
    }

    public Enemy CreateEnemy(EnemyTypeId id, Vector2 at, List<WayPoint> wayPoints)
    {
        EnemyStaticData data = _staticData.ForEnemy(enemyTypeId: id);
        Enemy enemy = Object.Instantiate(data.EnemyPrefab, position: at, Quaternion.identity);
        enemy.Attacker.Construct(data);
        enemy.Construct(data, wayPoints, _poolService);
        enemy.Sound.Construct(_handlers.Audio);
        enemy.RewardsSpawner.Construct(_poolService);

        _poolService.RegisterFactory(() => CreateDamageEffect());
        _poolService.RegisterFactory(() => CreateEnemyDeathEffect());
        _poolService.RegisterFactory(() => CreatePunchEffect());
        _poolService.RegisterFactory(() => CreateCoinReward());
        _poolService.RegisterFactory(() => CreateGemReward());

        if (id == EnemyTypeId.Range)
        {
            enemy.TryGetComponent(out EnemyBulletSpawner bulletSpawner);

            _poolService.RegisterFactory(() => CreateBomb());
            _poolService.RegisterFactory(() => CreateDamageArea());
            _poolService.RegisterFactory(() => CreateExplosion());

            bulletSpawner?.Construct(_poolService);
        }

        return enemy;
    }

    public Bus CreateBus(Vector2 at)
    {
        Bus bus = _assets.Instantiate(AssetsPath.BusPath, at: at)
            .GetComponent<Bus>();

        return bus;
    }

    public Bomb CreateBomb()
    {
        var bomb = _assets.Instantiate(AssetsPath.BombPath)
            .GetComponent<Bomb>();

        bomb.Construct(_poolService);
        return bomb;
    }

    public BombDamageArea CreateDamageArea()
    {
        var area = _assets.Instantiate(AssetsPath.BombAreaPath)
            .GetComponent<BombDamageArea>();

        area.Construct(_poolService);
        return area;
    }

    public BombYellow CreateExplosion()
    {
        BombYellow explosion = _assets.Instantiate(AssetsPath.BombExplosionPath)
            .GetComponent<BombYellow>();

        explosion.Construct(_poolService, _handlers.Audio);
        return explosion;
    }

    public DamageValueAnimation CreateDamageEffect()
    {
        DamageValueAnimation effect = _assets.Instantiate(AssetsPath.DamageEffectPath)
            .GetComponent<DamageValueAnimation>();

        effect.Construct(_poolService);

        return effect;
    }

    public EnemyDeathParticles CreateEnemyDeathEffect()
    {
        EnemyDeathParticles effect = _assets.Instantiate(AssetsPath.EnemyDeathPath)
            .GetComponent<EnemyDeathParticles>();

        effect.Construct(_poolService);

        return effect;
    }

    public PunchAnimation CreatePunchEffect()
    {
        PunchAnimation effect = _assets.Instantiate(AssetsPath.PunchEffectPath)
            .GetComponent<PunchAnimation>();

        effect.Construct(_poolService);

        return effect;
    }

    public Coin CreateCoinReward()
    {
        Coin coin = _assets.Instantiate(AssetsPath.RewardCoinPath)
            .GetComponent<Coin>();

        coin.Construct(_poolService, _progressService, _save);

        return coin;
    }

    public Gem CreateGemReward()
    {
        Gem gem = _assets.Instantiate(AssetsPath.RewardGemPath)
            .GetComponent<Gem>();

        gem.Construct(_poolService, _progressService, _save);

        return gem;
    }

    public List<EnemySpawner> CreateEnemySpawners(LevelData levelData)
    {
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

    private Key CreateKey(KeyData data, Color color, Inventory inventory)
    {
        Key key = _assets.Instantiate(AssetsPath.KeyPath, data.Position)
            .GetComponent<Key>();

        key.Construct(_handlers.Audio, color, inventory);

        return key;
    }
}