using YG;

public class SaveLoadService : ISaveLoadService
{
    private readonly IPersistentProgressService _persistentProgress;

    public SaveLoadService(IPersistentProgressService persistentProgress) =>
        _persistentProgress = persistentProgress;

    public void SaveProgress() =>
        YandexGamesSave();

    public SaveData LoadProgress() =>
        YG2.saves.Saves;

    private void YandexGamesSave()
    {
        YG2.saves.Saves = _persistentProgress.Progress;
        YG2.SaveProgress();
    }
}