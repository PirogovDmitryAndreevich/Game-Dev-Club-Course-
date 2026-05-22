public interface IStaticData : IService
{
    public void Load();
    EnemyStaticData ForEnemy(EnemyTypeId enemyTypeId);
    LevelData ForLevel(string sceneKey);
}
