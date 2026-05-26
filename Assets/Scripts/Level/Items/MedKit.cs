using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MedKit : Interactable, IInteractable
{
    [SerializeField] private AudioClip _sound;

    private AudioHandler _handler;
    private PlayerHealth _health;
    private int _value;

    public void Construct(AudioHandler handler, PlayerHealth health,int value)
    {
        _handler = handler;
        _health = health;
        _value = value;
    }

    public void Interact()
    {
        _handler.PlaySound(_sound);
        _health.Heal(_value);
        Destroy(gameObject);
    }
}