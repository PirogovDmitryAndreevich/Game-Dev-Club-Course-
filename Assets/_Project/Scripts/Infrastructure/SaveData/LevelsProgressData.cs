using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class LevelsProgressData
{
    public List<LevelSaveData> ReachedLevels;

    public event Action<SceneID> OpenedLevel;

    public LevelsProgressData()
    {
        ReachedLevels = new List<LevelSaveData>();
    }

    public void OpenNewLevel(SceneID id, int trophy)
    {
        var level = new LevelSaveData(id, trophy);
        ReachedLevels.Add(level);
        OpenedLevel?.Invoke(level.ID);
    }

    public bool Contain(SceneID id) =>
        ReachedLevels.Any(x => x.ID == id);

    public LevelSaveData GetSaveData(SceneID id) =>
        ReachedLevels.FirstOrDefault(x => x.ID == id);
}
