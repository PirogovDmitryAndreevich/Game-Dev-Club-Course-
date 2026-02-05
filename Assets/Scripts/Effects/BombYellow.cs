using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BombYellow : FXBase
{
    [SerializeField] private AudioClip _bombEffect;

    private ParticleSystem _particles;

    public override FXType Type => FXType.BombYellow;

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
        AudioManager.Instance.PlaySound(_bombEffect);
        _particles.Play();
    }
}
