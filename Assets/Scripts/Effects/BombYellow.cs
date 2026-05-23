using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BombYellow : MonoBehaviour
{
    [SerializeField] private AudioClip _bombSound;
    [SerializeField] private ParticleSystem _particles;

    private AudioHandler _handler;    
    private bool _isPlaying;
    private IPoolService _pool;

    private void Awake()
    {  
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
        _pool.Return(this);
    }

    public void Construct(IPoolService poolService, AudioHandler handler)
    {
        _handler = handler;
        _pool = poolService;
    }

    public void Play(Vector2 point)
    {
        transform.position = point;

        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _particles.Clear();

        _isPlaying = true;

        _handler.PlaySound(_bombSound);
        _particles.Play();
    }
}
