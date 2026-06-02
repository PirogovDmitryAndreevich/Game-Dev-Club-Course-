using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
public class LevelStaticData : ScriptableObject
{
    public List<LevelData> LevelData;
}
