using System;

public interface ITask
{
    public TaskType Type { get; }

    public event Action<ITask> TaskCompleted;
}
