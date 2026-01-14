using System.Collections;
using UnityEngine;

public class PinkAttackRealization : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _damageArea;

    [Header("Animation settings")]
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private int _damage;

    public void StartAttack(Vector2 position, int damage, LayerMask targetLayer)
    {
        var bullet = Instantiate(_bullet, transform.position,Quaternion.identity);
        var damageArea = Instantiate(_damageArea, position, Quaternion.identity);

        _damage = damage;

        StartCoroutine(FlyBullet(bullet, damageArea, targetLayer));
    }

    private IEnumerator FlyBullet(GameObject bullet, GameObject damageArea, LayerMask targetLayer)
    {
        Vector2 endScale = damageArea.transform.localScale;

        damageArea.transform.localScale = Vector2.zero;
        Vector2 startPosition = transform.position;

        CircleCollider2D damageCollider = damageArea.GetComponent<CircleCollider2D>();

        damageCollider.radius = endScale.x;

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
            damageCollider.radius,
            targetLayer
        );

        if (hit != null && hit.TryGetComponent(out Player player))
            player.ApplyDamage(_damage);

        Destroy(bullet);
        Destroy(damageArea);
    }
}
