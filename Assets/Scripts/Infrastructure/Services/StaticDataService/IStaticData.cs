public interface IStaticData : IService
{
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    void LoadEnemies();
}