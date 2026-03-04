using System.Collections;
using UnityEngine;

public class RewardsSpawner : MonoBehaviour
{
    private const int MaxSpawnCoins = 5;

    [SerializeField] private FXPool _pool;

    public void CreateCoins(int rewardValue, Vector2 spawnPoint)
    {
        if (rewardValue <= 0)
            return;

        int coinsToSpawn = CalculateSpawnCount(rewardValue);
        StartCoroutine(StartSpawn(rewardValue, coinsToSpawn, spawnPoint, FXType.Coin));
    }

    public void CreateGems(int rewardValue, Vector2 spawnPoint)
    {
        if (rewardValue <= 0)
            return;

        int gemsToSpawn = CalculateSpawnCount(rewardValue);
        StartCoroutine(StartSpawn(rewardValue, gemsToSpawn, spawnPoint, FXType.Gem));
    }

    private IEnumerator StartSpawn(int totalReward, int count, Vector2 point, FXType type)
    {
        int valuePerCoin = totalReward / count;
        int remainder = totalReward % count;

        for (int i = 0; i < count; i++)
        {
            if (i != 0)
                yield return new WaitForSeconds(0.1f);

            RewardForKillEnemy item;

            if (type == FXType.Coin)
                item = _pool.Get(FXType.Coin) as Coin;
            else
                item = _pool.Get(FXType.Gem) as Gem;

            int finalValue = valuePerCoin;

            if (remainder > 0)
            {
                finalValue += 1;
                remainder--;
            }

            item.SetReward(finalValue);
            item.Play(point);
        }
    }

    private int CalculateSpawnCount(int rewardValue)
    {
        int calculated = rewardValue / MaxSpawnCoins;

        if (calculated <= 0)
            calculated = 1;

        return Mathf.Min(calculated, MaxSpawnCoins);
    }
}
