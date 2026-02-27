using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const float MinEnemySeparationDistance = 0.000001f;
    private const float MinLengthMoveDirection = 0.01f;

    [Header("Move")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runSpeed = 6f;
    [Header("AttackStep")]
    [SerializeField] private float _attackStepDistance = 0.5f;
    [SerializeField] private float _speedAttackStep = 0.1f;
    [Header("Dash")]
    [SerializeField] private float _dashDistance = 5f;
    [SerializeField] private float _dashDuration = 0.15f;
    [SerializeField] private AnimationCurve _dashCurve;
    [Header("Take Damage")]
    [SerializeField] private float _knockbackDuration = 0.5f;
    [Header("EnemySeparationSetting")]
    [SerializeField] private float separationRadius = 1.2f;
    [SerializeField] private float separationStrength = 1.5f;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D _rigidbody;
    private Vector2 _lastMoveDirection = Vector2.zero;
    private Tween _currentTween;
    private Collider2D[] _neighbors = new Collider2D[16];

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.position = transform.position;
    }

    private void OnDestroy()
    {
        Stop();
    }

    public void Dash(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return;

        Stop();

        Vector2 target = _rigidbody.position + direction.normalized * _dashDistance;

        _currentTween = _rigidbody.DOMove(target, _dashDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(UpdateType.Fixed)
            .Play()
            .OnComplete(() => _currentTween = null);
    }

    public void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude > MinLengthMoveDirection)
            _lastMoveDirection = direction.normalized;

        _rigidbody.velocity = direction * _speed;
    }

    public void Run(Vector2 targetPoint) => Move(targetPoint, _runSpeed);
    public void RunAttack(Vector2 target, float attackSpeed) => Move(target, attackSpeed);
    public void Walk(Vector2 targetPoint) => Move(targetPoint, _speed);
    public void MoveToPoint(Vector2 point) => Move(point, _speed);

    public void AttackStep()
    {
        if (_lastMoveDirection == Vector2.zero)
            return;

        Stop();

        Vector2 target = _rigidbody.position + _lastMoveDirection * _attackStepDistance;

        _currentTween = _rigidbody.DOMove(target, _speedAttackStep)
            .SetEase(Ease.OutQuad)
            .Play()
            .OnComplete(() => _currentTween = null);

    }

    public void Knockback(Vector2 pushDirection, float knockbackForce)
    {
        Stop();

        pushDirection = new Vector2(Mathf.Sign(pushDirection.x), 0f);

        Vector2 startPos = _rigidbody.position;

        Vector2 target = startPos + pushDirection * knockbackForce;

        _currentTween = _rigidbody.DOMove(target, _knockbackDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(UpdateType.Fixed)
            .Play()
            .OnComplete(() => _currentTween = null);
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
        _currentTween?.Kill(false);
        _currentTween = null;
    }

    public Vector2 CalculateSeparation()
    {
        int countHits = Physics2D.OverlapCircleNonAlloc(
        _rigidbody.position,
        separationRadius,
        _neighbors,
        enemyLayer
    );

        Vector2 separation = Vector2.zero;
        int count = 0;

        for (int i = 0; i < countHits; i++)
        {
            var col = _neighbors[i];

            if (col.attachedRigidbody == _rigidbody)
                continue;

            Vector2 diff = _rigidbody.position - col.attachedRigidbody.position;

            float sqrDistance = diff.sqrMagnitude;
            if (sqrDistance <= MinEnemySeparationDistance)
                continue;

            float distance = Mathf.Sqrt(sqrDistance);
            float weight = 1f - (sqrDistance / (separationRadius * separationRadius));

            separation += diff.normalized * weight;
            count++;
        }

        if (count > 0)
            separation /= count;

        return separation * separationStrength;
    }

    private void Move(Vector2 targetPoint, float speed)
    {
        Vector2 moveDir = targetPoint - _rigidbody.position;
        Vector2 desired = moveDir.normalized;

        Vector2 separation = CalculateSeparation();

        Vector2 finalDir = (desired + separation).normalized;

        Vector2 newPosition = _rigidbody.position +
            finalDir * speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(newPosition);
    }
}
