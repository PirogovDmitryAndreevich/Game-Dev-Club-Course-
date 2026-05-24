using UnityEngine;

public class EnemyDeathParticles 
{
    private ParticleSystem _particles;


    private void Awake()
    {
        //_particles = GetComponent<ParticleSystem>();

        var main = _particles.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        _particles.Play();
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnParticleSystemStopped()
    {
       // ReturnToPool?.Invoke(this);
    }

    public  void Play(Vector2 point)
    {
        //transform.position = point;
        _particles.Play();
    }
}
