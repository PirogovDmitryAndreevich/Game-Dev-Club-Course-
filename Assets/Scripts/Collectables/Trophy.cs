using System;
using UnityEngine;

public class Trophy : Interactable
{
    [SerializeField] private AudioClip _sound;

    private AudioHandler _handler;

    public event Action<Trophy> Collected;

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
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}
