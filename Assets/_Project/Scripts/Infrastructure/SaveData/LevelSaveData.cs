using System;

[Serializable]
public class LevelSaveData
{
    public SceneID ID;
    public int Trophy;
    public int ReachedTrophy;

    public event Action<SceneID> TrophyChanged;

    public LevelSaveData(SceneID id, int trophy)
    {
        ID = id;
        Trophy = trophy;
        ReachedTrophy = 0;
    }

    public void UpdateReachedTrophy(int reached)
    {
        ReachedTrophy = reached;
        TrophyChanged?.Invoke(ID);
    }
}