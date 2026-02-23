using System;
using YG;

[Serializable]
public static class SaveData
{
    public static GameSaveData GameData { get; private set; } 
    public static PlayerSaveData PlayerData { get; private set; } 
    public static bool IsLoaded { get; private set; }

    public static event Action Loaded;
    
    public static void Save()
    {
        if (!IsLoaded)
            return;

        YG2.saves.GameSaveData = GameData;
        YG2.saves.PlayerSaveData = PlayerData;
        YG2.SaveProgress();
    }

    internal static void Load()
    {
        GameData = YG2.saves.GameSaveData ?? new GameSaveData();
        PlayerData = YG2.saves.PlayerSaveData ?? new PlayerSaveData();
        IsLoaded = true;
        Loaded?.Invoke();
    }
}
