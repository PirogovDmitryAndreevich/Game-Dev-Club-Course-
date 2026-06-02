using UnityEngine;

public class ArrowTracking : MonoBehaviour
{
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _radius;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;
    }

    private void OnDrawGizmos()
    {
        if (_player == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_player.position, _radius);
    }

    private void Update()
    {
        if (_player == null || _finishPoint == null || _radius <= 0f)
            return;

        UpdateArrow();
    }

    private void UpdateArrow()
    {
        if (_player == null || _finishPoint == null) return;

        Vector2 direction = (_finishPoint.position - _player.position).normalized;

        float flipCompensation = Mathf.Sign(_player.localScale.x);
        direction.x *= flipCompensation;

        _cachedTransform.localPosition = direction * _radius;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _cachedTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
