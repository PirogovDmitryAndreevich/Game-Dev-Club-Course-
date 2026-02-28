using System;
using UnityEngine;

public class Defense : Interactable, IInteractable, IShowKey
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private int _value = 10;

    public AudioClip DefenseSound => _sound;
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
