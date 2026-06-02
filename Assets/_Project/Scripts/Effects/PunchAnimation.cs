using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PunchAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private IPoolService _pool;

    public void Construct(IPoolService poolService)
    {
        _pool = poolService;
    }

    public void Play(Vector2 point) => 
        transform.position = point;

    // Called in animator
    private void OnAnimationEnded() => 
        _pool.Return(this);
}
