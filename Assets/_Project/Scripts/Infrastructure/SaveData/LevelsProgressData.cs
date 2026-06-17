using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class LevelsProgressData
{
    public ArenaSaveData ArenaSave;
    public List<LevelSaveData> ReachedLevels;

    public event Action<SceneID> OpenedLevel;

    public LevelsProgressData()
    {
        ReachedLevels = new List<LevelSaveData>();
        ArenaSave = new ArenaSaveData();
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

[Serializable]
public class ArenaSaveData
{
    public float RecordTime;
    public bool IsThereARecord;

    public event Action RecordChanged;

    public ArenaSaveData()
    {
        RecordTime = 0f;
        IsThereARecord = false;
    }

    public void UpdateRecord(float time)
    {
        RecordTime = time;
        IsThereARecord = true;

        RecordChanged?.Invoke();
    }
}