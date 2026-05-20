using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(AudioSource))]
public class BombYellow : MonoBehaviour
{
    [SerializeField] private AudioClip _bombEffect;

    private SaveData _progress;
    private AudioSource _bombSource;
    private ParticleSystem _particles;
    private bool _isPlaying;

    public Action ExplosionStopped;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        _bombSource = GetComponent<AudioSource>();
        _bombSource.playOnAwake = false;
        _bombSource.loop = false;

        var main = _particles.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);     
    }

    private void OnDisable()
    {
        _isPlaying = false;

        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particles.Clear();
    }

    private void OnParticleSystemStopped()
    {
        if (!_isPlaying)
            return;

        _isPlaying = false;

        ExplosionStopped?.Invoke();
    }

    public void Construct(IPersistentProgressService progressService)
    {
        _progress = progressService.Progress;
    }

    public void Play(Vector2 point)
    {
        transform.position = point;

        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particles.Clear();

        _isPlaying = true;

        _bombSource.volume = _progress.GameData.SoundValue;
        _bombSource.mute = _progress.GameData.IsSoundMute;

        _bombSource.PlayOneShot(_bombEffect);
        _particles.Play();
    }
}
