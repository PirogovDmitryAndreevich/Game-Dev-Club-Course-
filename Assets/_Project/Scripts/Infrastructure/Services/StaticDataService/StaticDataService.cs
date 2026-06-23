using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticData
{
    private const string EnemiesPath = "StaticData/Enemies";
    private const string LevelDataPath = "StaticData/LevelData";
    private const string PlayerDataPath = "StaticData/PlayerData";
    private const string ShopDataPath = "StaticData/ShopData";

    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private Dictionary<PlayerAttackType, AttackData> _playerAttacks;
    private PlayerStaticData _playerData;
    private ShopStaticData _shopData;

    public PlayerStaticData PlayerData => _playerData;
    public ShopStaticData ShopData => _shopData;
    public Dictionary<SceneID, LevelData> LevelsData { get; private set; }
    private Dictionary<DailyRewardType, DailyRewardStaticData> _dailyRewards { get; set; }

    public void Load()
    {
        _enemies = Resources.Load<AllEnemiesStaticData>(EnemiesPath)
            .Enemies.ToDictionary(x => x.TypeId, x => x);

        LevelsData = Resources.Load<LevelStaticData>(LevelDataPath)
            .LevelData.ToDictionary(x => x.ID, x => x);

        _playerData = Resources.Load<PlayerStaticData>(PlayerDataPath);

        _shopData = Resources.Load<ShopStaticData>(ShopDataPath);

        _dailyRewards = _shopData.DailyRewardsData.ToDictionary(x => x.Type, x => x);
        
        _playerAttacks = _playerData.Attacks.ToDictionary(x=>x.Type, x => x);
    }

    public EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out EnemyStaticData data)
        ? data
        : null;

    public LevelData ForLevel(SceneID id) =>
        LevelsData.TryGetValue(id, out LevelData data)
        ? data
        : null;

    public PlayerStaticData ForPlayer() =>
        _playerData;

    public AttackData ForPlayerAttack(PlayerAttackType type) =>
        _playerAttacks.TryGetValue(type, out AttackData data)
        ? data
        : null;

    public DailyRewardStaticData ForDailyReward(DailyRewardType type) =>
        _dailyRewards.TryGetValue(type, out DailyRewardStaticData data)
        ? data
        : null;
}
