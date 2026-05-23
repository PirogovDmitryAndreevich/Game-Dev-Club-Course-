using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    private IPoolService _pool;

    public void Construct(IPoolService poolService, IGameFactory gameFactory)
    {
        _pool = poolService;
        _pool.RegisterFactory(() => gameFactory.CreateBomb());
        _pool.RegisterFactory(() => gameFactory.CreateDamageArea());
        _pool.RegisterFactory(() => gameFactory.CreateExplosion());
    }

    public Bomb GetBomb() =>
        _pool.Get<Bomb>();

    public BombDamageArea GetDamageArea() =>
        _pool.Get<BombDamageArea>();
}