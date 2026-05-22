using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyTypeId _typeID;
    private List<WayPoint> _wayPoints;
    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory, EnemyTypeId typeID, List<WayPoint> wayPoints)
    {
        _gameFactory = gameFactory;
        _typeID = typeID;
        _wayPoints = wayPoints;
    }

    public Enemy Spawn() => 
        _gameFactory.CreateEnemy(_typeID, transform.position, _wayPoints);
}
