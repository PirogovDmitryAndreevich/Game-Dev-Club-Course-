using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "StaticData/Enemies")]
public class AllEnemiesStaticData : ScriptableObject
{
    public EnemyStaticData[] Enemies;
}