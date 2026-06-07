using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticData
{
    private const string EnemiesPath = "StaticData/Enemies";
    private const string LevelDataPath = "StaticData/LevelData";
    private const string PlayerDataPath = "StaticData/PlayerData";

    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private PlayerStaticData _playerData;

    public PlayerStaticData PlayerData => _playerData;
    public Dictionary<SceneID, LevelData> LevelsData { get; private set; }

    public void Load()
    {
        _enemies = Resources.Load<AllEnemiesStaticData>(EnemiesPath)
            .Enemies.ToDictionary(x => x.TypeId, x => x);

        LevelsData = Resources.Load<LevelStaticData>(LevelDataPath)
            .LevelData.ToDictionary(x => x.ID, x => x);

        _playerData = Resources.Load<PlayerStaticData>(PlayerDataPath);            
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
}
