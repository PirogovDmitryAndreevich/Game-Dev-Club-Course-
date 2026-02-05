using System;
using YG;

public static class SaveData
{
    public static GameSaveData GameData { get; private set; }
    public static PlayerSaveData PlayerData { get; private set; }
    public static bool IsLoaded { get; private set; }

    public static event Action OnLoaded;

    internal static void Load()
    {
        GameData = YG2.saves.GameSaveData ?? new GameSaveData();
        PlayerData = YG2.saves.PlayerSaveData ?? new PlayerSaveData();
        IsLoaded = true;
        OnLoaded?.Invoke();
    }

    public static void Save()
    {
        if (!IsLoaded)
            return;

        YG2.saves.GameSaveData = GameData;
        YG2.saves.PlayerSaveData = PlayerData;
        YG2.SaveProgress();
    }
}
