using UnityEngine;

public class RewardsSpawner : MonoBehaviour
{
    private const int MinReward = 1;
    private IPoolService _pool;

    public void Construct(IPoolService poolService) => 
        _pool = poolService;

    public void CreateCoins(int rewardValue)
    {
        if (rewardValue <= 0)
            return;

        int coinsToSpawn = CalculateSpawnCount(rewardValue);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            var coin = _pool.Get<Coin>();
            coin.Spawn(transform.position);
        }
    }

    public void CreateGems()
    {
        var gem = _pool.Get<Gem>();
        gem.Spawn(transform.position);
    }

    private int CalculateSpawnCount(int rewardValue) =>
        Mathf.Max(MinReward, rewardValue / 100);
}
