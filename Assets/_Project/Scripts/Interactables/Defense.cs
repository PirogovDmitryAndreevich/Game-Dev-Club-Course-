using System;
using UnityEngine;

public class Defense : Interactable, IInteractable
{
    [SerializeField] private AudioClip _sound;

    private AudioHandler _handler;
    private PlayerDefense _defense;
    private int _value;

    public void Construct(AudioHandler handler, PlayerDefense defense,int value)
    {
        _handler = handler;
        _defense = defense;
        _value = value;
    }

    public void Interact()
    {
        _handler.PlaySound(_sound);
        _defense.AddDefense(_value);
        Destroy(gameObject);
    }

}
