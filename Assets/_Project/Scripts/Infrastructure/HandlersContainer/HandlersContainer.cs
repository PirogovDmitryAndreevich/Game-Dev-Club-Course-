public class HandlersContainer : IHandlersContainer
{
    private readonly IPersistentHandlersFactory _factory;

    public HandlersContainer(IPersistentHandlersFactory factory)
    {
        _factory = factory;
    }

    public AudioHandler Audio { get; private set; }

    public void CreateHandlers()
    {
        Audio = _factory.CreateAudioHandler();        
    }
}