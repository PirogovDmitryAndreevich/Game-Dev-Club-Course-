public interface IHandlersContainer : IService
{
    AudioHandler Audio { get;}
    public void CreateHandlers();
}