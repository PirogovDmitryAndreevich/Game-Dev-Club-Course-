using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyDeathParticles : FXBase
{
    private ParticleSystem _particles;
    public override FXType Type => FXType.EnemyDeath;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();

        var main = _particles.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        _particles.Play();
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnParticleSystemStopped()
    {
        ReturnToPool();
    }

    public override void Play(Vector2 point)
    {
        transform.position = point;
        _particles.Play();
    }
}
