using Cinemachine;
using UnityEngine;

public interface IGameFactory : IService
{
    Player CreatePlayerHero(Vector2 at, CinemachineVirtualCamera camera);
    CinemachineVirtualCamera CreateVirtualCamera();
    Enemy CreateEnemy(EnemyTypeId id, Vector2 at, WayPoint[] wayPoints);
}