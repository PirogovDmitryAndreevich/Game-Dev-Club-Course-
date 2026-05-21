using System.Collections.Generic;

public interface IScenesLogicContainer : IService
{
    Dictionary<SceneID, IScene> Scenes { get; }

    void AddNewScene(SceneID id, IScene scene);
    void Cleanup();
}