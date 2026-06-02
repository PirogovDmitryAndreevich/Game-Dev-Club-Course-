using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/Player")]
public class PlayerStaticData : ScriptableObject
{
    public int SuperHitCount = 3;
    public float ComboCooldown = 1.2f;
    public float CooldownTime;

    public Attack[] Attacks;
}