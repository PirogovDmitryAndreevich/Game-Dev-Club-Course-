using DG.Tweening;
using System;
using UnityEngine;

public class Bus : MonoBehaviour
{
    private const string StartMovingAnimation = "StartMove";

    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private bool _isFromTheRight;

    private Tween _moveTween;

    public event Action MovementCompleted; 

    private void OnEnable()
    {
        Vector3 scale = transform.localScale;

        if (!_isToRight)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    private void OnDestroy()
    {
        _moveTween?.Kill();
    }

    public void StartToBusStop(Vector2 busStopPosition) => Move(busStopPosition);

    public void StartGoOut(Vector2 endPosition) => Move(endPosition);

    private void Move(Vector2 target)
    {
        StopMove();

        float distance = Mathf.Abs(transform.position.x - target.x);
        float duration = distance / _moveSpeed;

        _animator.SetBool(StartMovingAnimation, true);

        _moveTween = transform.DOMoveX(target.x, duration)
            .SetEase(Ease.Linear)
            .Play()
            .OnComplete(() =>
            {
                StopMove();
                MovementCompleted?.Invoke();
            }
                );
    }

    private void StopMove()
    {
        _moveTween?.Kill(false);
        _moveTween = null;

        _animator.SetBool(StartMovingAnimation, false);
    }
}
