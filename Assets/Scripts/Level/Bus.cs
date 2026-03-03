using DG.Tweening;
using System;
using UnityEngine;

public class Bus : MonoBehaviour
{
    private const string StartMovingAnimation = "StartMove";

    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed = 5f;

    private Tween _moveTween;

    public event Action MovementCompleted; 

    private void OnDestroy()
    {
        _moveTween?.Kill();
    }

    public void StartToBusStop(Vector2 busStopPosition) => Move(busStopPosition);

    public void StartGoOut(Vector2 endPosition) => Move(endPosition);

    public void SetFlip(bool isFromRight)
    {
        Vector3 scale = transform.localScale;

        if (!isFromRight)
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

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
