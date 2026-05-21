using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticData
{
    private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;

    public void LoadEnemies()
    {
        _enemies = Resources.Load<AllEnemiesStaticData>("StaticData/Enemies")
            .Enemies.ToDictionary(x => x.TypeId, x => x);
    }

    public EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId) =>
        _enemies.TryGetValue(enemyTypeId, out EnemyStaticData data)
        ? data
        : null;
}
