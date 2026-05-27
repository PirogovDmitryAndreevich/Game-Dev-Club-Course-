using System;
using UnityEngine;

public class Trophy : Interactable
{
    [SerializeField] private AudioClip _sound;

    private AudioHandler _handler;

    public AudioClip TrophySound => _sound;
    public TaskType Type => TaskType.Trophy;

    public event Action<ITask> TaskCompleted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))        
            Collect();        
    }

    public void Construct(AudioHandler handler)
    {
        _handler = handler;
    }

    public void Collect()
    {
        _handler.PlaySound(_sound);
        Destroy(gameObject);
    }
}
