using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    Player CreatePlayerHero(Vector2 at, CinemachineVirtualCamera camera);
    CinemachineVirtualCamera CreateVirtualCamera();
    Enemy CreateEnemy(EnemyTypeId id, Vector2 at, List<WayPoint> wayPoints);
    List<EnemySpawner> CreateEnemySpawners(LevelData levelData);
    Bomb CreateBomb();
    BombDamageArea CreateDamageArea();
    BombYellow CreateExplosion();
    DamageValueAnimation CreateDamageEffect();
}