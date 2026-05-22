using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticData
{
    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
    private Dictionary<string, LevelData> _levels;
    private PlayerStaticData _playerData;

    public void Load()
    {
        _enemies = Resources.Load<AllEnemiesStaticData>("StaticData/Enemies")
            .Enemies.ToDictionary(x => x.TypeId, x => x);

        _levels = Resources.Load<LevelStaticData>("StaticData/LevelData")
            .LevelsGraveyard.ToDictionary(x => x.LevelKey, x => x);

        _playerData = Resources.Load<PlayerStaticData>("StaticData/PlayerData");            
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
