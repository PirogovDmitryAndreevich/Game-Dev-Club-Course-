using UnityEngine;

public class EnemyDeathParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    private IPoolService _pool;

    private void Awake()
    {
        var main = _particles.main;
        main.stopAction = ParticleSystemStopAction.Callback;

        _particles.Play();
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void OnParticleSystemStopped() => 
        _pool.Return(this);

    public void Construct(IPoolService poolService) => 
        _pool = poolService;

    public void Play(Vector2 point)
    {
        transform.position = point;
        _particles.Play();
    }
}
