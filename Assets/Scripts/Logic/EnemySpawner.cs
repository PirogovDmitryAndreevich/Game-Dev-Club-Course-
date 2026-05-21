using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyTypeId _typeID;
    [SerializeField] private WayPoint[] _wayPoints;

    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public Enemy Spawn() => 
        _gameFactory.CreateEnemy(_typeID, transform.position, _wayPoints);
}
