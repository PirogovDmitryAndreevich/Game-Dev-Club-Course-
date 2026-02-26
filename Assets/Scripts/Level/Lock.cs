using UnityEngine;
using DG.Tweening;

public class Lock : MonoBehaviour, IInteractable, IShowKey
{
    [SerializeField] private SpriteRenderer _padlock;
    [SerializeField] private Transform _barriers;
    [SerializeField] private Key _key;
    [SerializeField] private bool _isLock;

    [Header("Animation settings")]
    [SerializeField] private float jumpUpDistance = 150f;
    [SerializeField] private float moveRightDistance = 60f;
    [SerializeField] private float rotateAmount = 25f;
    [SerializeField] private float downDistance = 0.5f;

    private Transform _padlockTransform;
    private Transform[] _barrierPieces;
    Sequence _animation;

    public bool IsLock => _isLock;
    public Key Key => _key;
    private bool IsAnimating => _animation != null && _animation.active;

    private void Awake()
    {
        _padlockTransform = _padlock.GetComponent<Transform>();
        _barrierPieces = _barriers.GetComponentsInChildren<Transform>();
        _key.ColorKey = _padlock.color;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 startPosBarriers = _barriers.transform.position;
        Vector3 upRightPosBarriers = startPosBarriers + Vector3.up * jumpUpDistance;
        Gizmos.DrawLine(transform.position, upRightPosBarriers);
        Vector3 fallPosBarriers = upRightPosBarriers + Vector3.down * downDistance;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(upRightPosBarriers, fallPosBarriers);
    }

    private void OnDestroy()
    {
        KillAnimations();
    }

    public void Interact()
    {
        PlayUnlockAnimationAndDestroy();
    }

    private void PlayUnlockAnimationAndDestroy()
    {
        KillAnimations();

        _animation = DOTween.Sequence();

        _padlockTransform.localScale = Vector3.one;
        _barriers.localScale = Vector3.one;

        Vector3 startBarrierPos = _barriers.position;
        Vector3 upPos = startBarrierPos + Vector3.up * (jumpUpDistance * 0.5f);

        _animation.Append(
            _padlockTransform
                .DOScale(0.9f, 0.08f)
                .SetEase(Ease.OutQuad)
        );

        _animation.Append(
            _padlockTransform
                .DOScale(1.15f, 0.1f)
                .SetEase(Ease.OutBack)
        );

        _animation.Join(
            _padlockTransform
                .DORotate(new Vector3(0, 0, -rotateAmount), 0.15f)
                .SetEase(Ease.OutQuad)
        );

        _animation.Join(
            _padlockTransform
                .DOMoveX(_padlockTransform.position.x + moveRightDistance, 0.2f)
                .SetEase(Ease.OutExpo)
        );

        _animation.Join(
            _padlock
                .DOFade(0f, 0.2f)
                .SetEase(Ease.OutQuad)
        );

        _animation.Append(
            _barriers
                .DOScale(1.1f, 0.1f)
                .SetEase(Ease.OutQuad)
);

        _animation.Append(
            _barriers
                .DOScale(1f, 0.08f)
                .SetEase(Ease.InQuad)
        );

        foreach (var piece in _barrierPieces)
        {
            if (piece == _barriers)
                continue;

            Vector3 randomOffset = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(0.3f, 0.8f),
                0f
            );

            _animation.Join(
                piece
                    .DOLocalMove(piece.localPosition + randomOffset, 0.2f)
                    .SetEase(Ease.OutQuad)
            );

            _animation.Join(
                piece
                    .DORotate(new Vector3(0, 0, Random.Range(-90f, 90f)), 0.25f)
                    .SetEase(Ease.OutQuad)
            );
        }

        _animation.Append(
            _barriers
                .DOScale(0f, 0.2f)
                .SetEase(Ease.InBack)
);

        _animation.Play();

        _animation.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void KillAnimations()
    {
        if (IsAnimating)
            _animation.Kill();
    }
}
