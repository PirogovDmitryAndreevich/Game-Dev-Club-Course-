using UnityEngine;

public class MedKit : Interactable, IInteractable, IShowKey
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private int _value = 10;

    public AudioClip MedKitSound => _sound;
    public int Value => _value;

    private void OnEnable()
    {
        Moving();
    }

    public void Interact()
    {        
        Destroy(gameObject);
    }
}