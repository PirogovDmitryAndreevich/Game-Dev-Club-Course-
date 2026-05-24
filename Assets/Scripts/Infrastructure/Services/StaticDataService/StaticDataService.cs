using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticData
{
    private const string EnemiesPath = "StaticData/Enemies";
    private const string LevelDataPath = "StaticData/LevelData";
    private const string PlayerDataPath = "StaticData/PlayerData";

    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private Dictionary<string, LevelData> _levels;
    private PlayerStaticData _playerData;

    public void Load()
    {
        _enemies = Resources.Load<AllEnemiesStaticData>(EnemiesPath)
            .Enemies.ToDictionary(x => x.TypeId, x => x);

        _levels = Resources.Load<LevelStaticData>(LevelDataPath)
            .LevelData.ToDictionary(x => x.LevelKey, x => x);

        _playerData = Resources.Load<PlayerStaticData>(PlayerDataPath);            
    }

    public EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out EnemyStaticData data)
        ? data
        : null;

    public LevelData ForLevel(string sceneKey) =>
        _levels.TryGetValue(sceneKey, out LevelData data)
        ? data
        : null;

    public PlayerStaticData ForPlayer() =>
        _playerData;
}
