using UnityEngine;
using DG.Tweening;

public class Lock : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _padlock;
    [SerializeField] private Transform _padlockTransform;
    [SerializeField] private Transform _barriers;
    [SerializeField] private Transform[] _barrierPieces;
    [SerializeField] private AudioClip _negativeSound;
    [SerializeField] private AudioClip _unlockSound;

    [Header("Animation settings")]
    [SerializeField] private float jumpUpDistance = 150f;
    [SerializeField] private float moveRightDistance = 60f;
    [SerializeField] private float rotateAmount = 25f;

    private Key _key;
    private Inventory _inventory;
    private Sequence _animation;

    private bool IsAnimating => _animation != null && _animation.active;
    public bool IsActivated { get; private set; } = false;

    private void OnDestroy() =>
        KillAnimations();

    public void Construct(Key key, Color color, Inventory inventory)
    {
        _key = key;
        _inventory = inventory;
        _padlock.color = color;
    }

    public void Interact()
    {
        if (_inventory.Contains(_key))
            PlayUnlockAnimationAndDestroy();
        else
            PlayLockAnimation();

    }

    private void PlayLockAnimation()
    {
        
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
            IsActivated = true;
            _inventory.RemoveKey(_key);
            Destroy(gameObject);
        });
    }

    private void KillAnimations()
    {
        if (IsAnimating)
            _animation.Kill();
    }
}
