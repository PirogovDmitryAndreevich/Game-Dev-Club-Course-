using System;

[Serializable]
public class SaveData
{
    public GameSaveData GameData;
    public PlayerSaveData PlayerData;
    public LevelsProgressData LevelsProgress;

    public SaveData()
    {
        GameData = new GameSaveData();
        PlayerData = new PlayerSaveData();
        LevelsProgress = new LevelsProgressData();
    }
}
