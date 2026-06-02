using System;
using System.Collections.Generic;

[Serializable]
public class LevelsProgressData
{
    public List<SceneID> ReachedLevels;

    public event Action<SceneID> OpenedLevel;

    public LevelsProgressData()
    {
        ReachedLevels = new List<SceneID>();
        ReachedLevels.Add(SceneID.Level_1);
    }

    public void OpenNewLevel(SceneID id)
    {
        ReachedLevels.Add(id);
        OpenedLevel?.Invoke(id);
    }
}