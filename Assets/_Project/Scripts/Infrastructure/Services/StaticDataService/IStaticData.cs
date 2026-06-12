using System.Collections.Generic;

public interface IStaticData : IService
{
    public Dictionary<SceneID, LevelData> LevelsData { get; }
    public PlayerStaticData PlayerData { get; }

    public ShopStaticData ShopData { get; }
    public void Load();
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelData ForLevel(SceneID id);
    PlayerStaticData ForPlayer();
    AttackData ForPlayerAttack(PlayerAttackType type);
}
