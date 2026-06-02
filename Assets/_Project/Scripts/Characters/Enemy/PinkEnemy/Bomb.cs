using DG.Tweening;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private const float TargetOffset = 0.8f;

    private Tween _animation;
    private Transform _transform;
    private IPoolService _pool;

    public void Construct(IPoolService poolService)
    {
        _transform = transform;
        _pool = poolService;
    }

    public void Play( Vector2 point, Vector2 target, float duration)
    {
        _transform.position = point;
        target.y -= TargetOffset;

        _animation = _transform.DOMove(target , duration)
            .SetEase(Ease.Linear)
            .Play()
            .OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        _animation?.Kill();
        _pool.Return(this);
    }
}
