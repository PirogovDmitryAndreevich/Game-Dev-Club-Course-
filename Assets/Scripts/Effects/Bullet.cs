using System.Collections;
using UnityEngine;

public class Bullet : FXBase
{
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _damageArea;

    [Header("Animation settings")]
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public override FXType Type => FXType.Bullet;

    CircleCollider2D _damageCollider;

    private void Awake()
    {
        _damageCollider = _damageArea.GetComponent<CircleCollider2D>();
    }

    public override void Play(Vector2 point)
    {
        throw new System.NotImplementedException();
    }

    public void Play(Vector2 point, Vector2 target, AttackBase attack, LayerMask targetLayer)
    {
        _bullet.transform.position = point;
        _damageArea.transform.position = target;

        StartCoroutine(FlyBullet(_bullet, _damageArea, attack, targetLayer));
    }

    private IEnumerator FlyBullet(GameObject bullet, GameObject damageArea, AttackBase attack ,LayerMask targetLayer)
    {
        Vector2 endScale = damageArea.transform.localScale;
        damageArea.transform.localScale = Vector2.zero;

        Vector2 startPosition = bullet.transform.position;        

        _damageCollider.radius = endScale.x;

        float elapsed = 0f;

        while (elapsed < _animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / _animationDuration;

            float scaleValue = _scaleCurve.Evaluate(t);
            damageArea.transform.localScale = Vector2.one * scaleValue;

            bullet.transform.position = Vector2.Lerp(startPosition, damageArea.transform.position, t);

            yield return null;
        }

        Collider2D hit = Physics2D.OverlapCircle(
            damageArea.transform.position,
            _damageCollider.radius,
            targetLayer
        );

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            Vector2 knockbackDir =
                ((Vector2)player.transform.position - (Vector2)damageArea.transform.position)
                .normalized;
            player.ApplyDamage(attack, damageArea.transform.position, knockbackDir);
        }

        var bombEffect = FXPool.Instance.Get(FXType.BombYellow);
        bombEffect.Play(bullet.transform.position);

        ReturnToPool();
    }
}
