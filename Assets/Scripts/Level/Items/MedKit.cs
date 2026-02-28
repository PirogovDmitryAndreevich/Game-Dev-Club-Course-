using UnityEngine;

public class MedKit : Interactable, IInteractable, IShowKey
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private int _value = 10;

    public AudioClip MedKitSound => _sound;
    public int Value => _value;

    public bool IsActivated { get; private set; }

    private void OnEnable()
    {
        IsActivated = false;
        Moving();
    }

    public void Interact()
    {
        IsActivated = true;
        Destroy(gameObject);
    }
}