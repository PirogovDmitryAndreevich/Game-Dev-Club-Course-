using System.Collections.Generic;

public interface IStaticData : IService
{
    public Dictionary<SceneID, LevelData> LevelsData { get; }
    public void Load();
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelData ForLevel(SceneID id);
    PlayerStaticData ForPlayer();
}
