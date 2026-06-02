using System;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _healEffect;
    [SerializeField] private ParticleSystem _defenseEffect;

    private PlayerHealth _heals;
    private PlayerDefense _defense;

    private void OnDestroy()
    {
        _heals.Healed -= PlayHeal;
        _defense.AddedDefense -= PlayAddingArmor;
    }

    public void Construct(PlayerHealth health, PlayerDefense defense)
    {
        _heals = health;
        _defense = defense;

        _heals.Healed += PlayHeal;
        _defense.AddedDefense += PlayAddingArmor;
    }

    public void PlayAddingArmor() => 
        _defenseEffect.Play();

    public void PlayHeal() => 
        _healEffect.Play();
}
