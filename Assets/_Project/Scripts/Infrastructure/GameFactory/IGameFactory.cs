using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    Player CreatePlayerHero(Vector2 at, CinemachineVirtualCamera camera);
    CinemachineVirtualCamera CreateVirtualCamera();
    Lock CreateLock(LockData data, Inventory inventory);
    Trophy CreateTrophy(Vector2 at);
    MedKit CreateMedKit(Vector2 at, PlayerHealth health, int value);
    Defense CreateDefense(Vector2 at, PlayerDefense playerDefense, int value);
    Enemy CreateEnemy(EnemyTypeId id, Vector2 at, List<WayPoint> wayPoints);
    List<EnemySpawner> CreateEnemySpawners(LevelData levelData);
    Bomb CreateBomb();
    BombDamageArea CreateDamageArea();
    BombYellow CreateExplosion();
    DamageValueAnimation CreateDamageEffect();
    BusStop CreateBusStop(Vector2 position);
    Bus CreateBus(Vector2 at, Vector2 endMovePoint, bool isFromRight);
}