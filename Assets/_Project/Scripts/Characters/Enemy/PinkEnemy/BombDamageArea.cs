using DG.Tweening;
using System.Linq;
using UnityEngine;

public class BombDamageArea : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _scaleCurve =
        AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private Transform _view;
    [SerializeField] private CapsuleCollider2D _damageCollider;

    private IPoolService _pool;
    private Tween _animation;
    private Collider2D[] _hits = new Collider2D[1];

    public void Construct(IPoolService poolService) =>
        _pool = poolService;

    public void Play(Vector2 target, float duration, int damage, float knockbackForce, LayerMask targetLayer)
    {
        transform.position = target;
        _hits[0] = null;

        Vector2 finalScale = _view.localScale;
        _view.localScale = Vector2.zero;

        _animation = _view.DOScale(finalScale, duration).From(_view.localScale)
            .SetEase(_scaleCurve)
            .Play()
            .OnComplete(() =>
            {
                ApplyDamage(damage, knockbackForce, targetLayer);
                OnComplete();
            });
    }

    private void ApplyDamage(int damage, float knockbackForce, LayerMask targetLayer)
    {
        Vector2 position = _damageCollider.bounds.center;
        Vector2 size = _damageCollider.bounds.size;

        Physics2D.OverlapCapsuleNonAlloc(
            position,
            size,
            _damageCollider.direction,
            0f,
            _hits,
            targetLayer
        );

        Collider2D hit = _hits.FirstOrDefault();

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            Vector2 knockbackDir =
                ((Vector2)player.transform.position - position).normalized;

            player.ApplyDamage(damage, knockbackForce, position, knockbackDir);
        }
    }

    private void OnComplete()
    {
        _animation?.Kill();
        BombYellow explosion = _pool.Get<BombYellow>();
        explosion.Play(transform.position);
        _pool.Return(this);
    }
}