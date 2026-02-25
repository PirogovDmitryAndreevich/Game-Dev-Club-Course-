using System;

public class Trophy : Interactable, IItem, ITask
{
    public TaskType Type => TaskType.Trophy;

    public event Action<ITask> TaskCompleted;

    private void OnEnable()
    {
        Moving();
    }

    public void Collect()
    {
        TaskCompleted?.Invoke(this);
        Destroy(gameObject);
    }

}
