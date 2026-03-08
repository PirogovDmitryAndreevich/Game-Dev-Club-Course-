using System;
using UnityEngine;

public class Trophy : Interactable, IItem, ITask
{
    [SerializeField] private AudioClip _sound;

    public AudioClip TrophySound => _sound;
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
