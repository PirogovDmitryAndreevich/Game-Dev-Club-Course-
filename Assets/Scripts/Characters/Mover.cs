using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runSpeed = 6f;
    [Header("Attack")]
    [SerializeField] private float _attackStepDistance = 0.5f;
    [SerializeField] private float _attackStepDuration = 0.1f;
    [Header("Dash")]
    [SerializeField] private float _dashDistance = 5f;
    [SerializeField] private float _dashDuration = 0.15f;
    [SerializeField] private AnimationCurve _dashCurve;
    [Header("Take Damage")]
    [SerializeField] private float _knockbackForce = 1f;
    [SerializeField] private float _knockbackDuration = 0.5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _lastMoveDirection = Vector2.zero;
    private Tween _currentTween;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Dash(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return;

        Stop(); // убиваем текущие tween и velocity

        Vector2 target = _rigidbody.position + direction.normalized * _dashDistance;

        _currentTween = _rigidbody.DOMove(target, _dashDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(UpdateType.Fixed)
            .Play()
            .OnComplete(() => _currentTween = null);
    }

    public void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
            _lastMoveDirection = direction.normalized;

        _rigidbody.velocity = direction * _speed;
    }

    public void Run(Transform target) => Move(target, _runSpeed);

    public void RunAttack(Vector2 target, float attackSpeed) => Move(target, attackSpeed);

    public void Walk(Transform target) => Move(target, _speed);

    private void Move(Transform target, float speed)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    public void AttackStep()
    {
        if (_lastMoveDirection == Vector2.zero) return;

        Stop();

        Debug.Log(_lastMoveDirection);
        Vector2 target = _rigidbody.position + _lastMoveDirection * _attackStepDistance;

        _currentTween = _rigidbody.DOMove(target, _attackStepDuration)
            .SetEase(Ease.OutQuad)
            .Play()
            .OnComplete(() => _currentTween = null);

    }

    public void TakeDamage(Vector2 pushDirection)
    {
        Stop();

        pushDirection = new Vector2(Mathf.Sign(pushDirection.x), 0f);

        Vector2 startPos = _rigidbody.position;

        Vector2 target = startPos + pushDirection * _knockbackForce;

        _currentTween = _rigidbody.DOMove(target, _knockbackDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(UpdateType.Fixed)
            .Play()
            .OnComplete(() => _currentTween = null);
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
        _currentTween?.Kill();
        _currentTween = null;
    }

    private void Move(Vector2 target, float speed)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }
}
