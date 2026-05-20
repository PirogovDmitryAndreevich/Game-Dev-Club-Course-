public class PersistentHandlersFactory : IPersistentHandlersFactory
{
    private readonly IAssets _assets;
    private readonly IPersistentProgressService _persistentProgress;

    public PersistentHandlersFactory(IAssets assets, IPersistentProgressService persistentProgress)
    {
        _assets = assets;
        _persistentProgress = persistentProgress;
    }

    public AudioHandler CreateAudioHandler()
    {
        AudioHandler manager = _assets.Instantiate(AssetsPath.AudioManagerPath)
            .GetComponent<AudioHandler>();

        manager.Construct(_persistentProgress);

        return manager;
    }
}