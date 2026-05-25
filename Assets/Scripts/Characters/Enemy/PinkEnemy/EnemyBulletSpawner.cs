using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    private IPoolService _pool;

    public void Construct(IPoolService poolService) => 
        _pool = poolService;

    public Bomb GetBomb() =>
        _pool.Get<Bomb>();

    public BombDamageArea GetDamageArea() =>
        _pool.Get<BombDamageArea>();
}