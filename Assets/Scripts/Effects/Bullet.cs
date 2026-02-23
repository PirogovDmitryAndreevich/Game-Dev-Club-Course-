using System.Collections;
using UnityEngine;

public class Bullet : FXBase
{
    [SerializeField] private Transform _bullet;
    [SerializeField] private Transform _damageArea;
    [SerializeField] private BombYellow _explosion;

    [Header("Animation settings")]
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private CircleCollider2D _damageCollider;
    private Coroutine _flyRoutine;
    private Collider2D[] _overlapResults = new Collider2D[8];

    public override FXType Type => FXType.Bullet;

    private void Awake()
    {
        _damageCollider = _damageArea.GetComponent<CircleCollider2D>();

        _explosion.ExplosionStopped += OnExplosionStopped;
    }

    private void OnDisable()
    {
        if (_flyRoutine != null)
            StopCoroutine(_flyRoutine);

        _flyRoutine = null;
    }

    private void OnDestroy()
    {
        _explosion.ExplosionStopped -= OnExplosionStopped;
    }

    public override void Play(Vector2 point)
    {
        throw new System.NotImplementedException();
    }

    public void Play(Vector2 point, Vector2 target, AttackBase attack, LayerMask targetLayer)
    {
        if (_flyRoutine != null)
            StopCoroutine(_flyRoutine);

        _bullet.transform.position = point;
        _damageArea.transform.position = target;

        _flyRoutine = StartCoroutine(FlyBullet(_bullet, _damageArea, attack, targetLayer));
    }

    private IEnumerator FlyBullet(Transform bullet, Transform damageArea, AttackBase attack ,LayerMask targetLayer)
    {
        Vector2 endScale = _damageArea.localScale;
        damageArea.localScale = Vector2.zero;

        Vector2 startPosition = bullet.position;        

        _damageCollider.radius = endScale.x;

        float elapsed = 0f;

        while (elapsed < _animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / _animationDuration;

            float scaleValue = _scaleCurve.Evaluate(t);
            damageArea.transform.localScale = Vector2.one * scaleValue;

            bullet.position = Vector2.Lerp(startPosition, damageArea.position, t);

            yield return null;
        }

        int hitCount = Physics2D.OverlapCircleNonAlloc(
            damageArea.position,
            _damageCollider.radius,
            _overlapResults,
            targetLayer
        );

        for (int i = 0; i < hitCount; i++)
        {
            if (_overlapResults[i].TryGetComponent(out Player player))
            {
                Vector2 knockbackDir =
                    ((Vector2)player.transform.position - (Vector2)damageArea.position)
                    .normalized;

                player.ApplyDamage(attack, damageArea.position, knockbackDir);
                break; 
            }
        }

        _explosion.Play(bullet.position);
    }

    private void OnExplosionStopped()
    {
        ReturnToPool?.Invoke(this);
    }
}
