using System;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _healEffect;
    [SerializeField] private ParticleSystem _defenseEffect;

    internal void PlayAddingArmor()
    {
        _defenseEffect.Play();
    }

    internal void PlayHeal()
    {
        _healEffect.Play();
    }
}
