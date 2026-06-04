using UnityEngine;

public class ArrowTracking : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Fliper _flipper;
    [SerializeField] private Transform _view;
    [SerializeField] private float _radius;

    private Transform _target;
    private Transform _cachedTransform;

    private void Awake() => 
        _cachedTransform = transform;

    private void OnDrawGizmos()
    {
        if (_player == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_player.position, _radius);
    }

    private void Update()
    {
        if (_player == null || _target == null || _radius <= 0f)
        {
            _view.gameObject.SetActive(false);
            return;
        }

        UpdateArrow();
    }

    public void SetTarget(Transform target) => 
        _target = target;

    private void UpdateArrow()
    {
        Vector2 target = _target.position - _player.position;

        Vector2 direction = target.normalized;
        float sqrDistance = target.sqrMagnitude;

        _view.gameObject.SetActive(sqrDistance > _radius * _radius);

        if (_flipper.IsTernRight)
            direction.x *= -1;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _cachedTransform.localPosition = direction * _radius;
        _cachedTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
