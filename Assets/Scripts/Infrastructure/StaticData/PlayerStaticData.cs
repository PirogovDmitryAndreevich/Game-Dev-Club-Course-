using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/Player")]
public class PlayerStaticData : ScriptableObject
{
    public float CooldownTime;
    public float AttackOffset;
    public float AttackRadius;
}