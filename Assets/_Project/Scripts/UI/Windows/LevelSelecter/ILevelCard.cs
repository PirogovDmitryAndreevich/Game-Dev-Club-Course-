using System;

public interface ILevelCard
{
    SceneID LevelID { get; }

    event Action<ILevelCard> Selected;

    void Deselect();
    void Select();
}