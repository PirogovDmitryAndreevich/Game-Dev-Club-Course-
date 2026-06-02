using System.Collections.Generic;

public class ScenesLogicContainer : IScenesLogicContainer
{
    public Dictionary<SceneID, IScene> Scenes { get; private set; }

    public ScenesLogicContainer()
    {
        Scenes = new Dictionary<SceneID, IScene>();
    }

    public void AddNewScene(SceneID id, IScene scene)
    {
        if (!Scenes.ContainsKey(id))
            Scenes[id] = scene;
    }

    public void Cleanup() =>
        Scenes.Clear();
}