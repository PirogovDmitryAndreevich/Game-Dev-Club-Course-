using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class RewardForKillEnemy : FXBase, IItem
{
    private const float MinJumpDistance = 1f;

    [Header("Base settings")]
    [SerializeField] private AudioClip _sound;
    [SerializeField] private Transform _view;
    [SerializeField] private FXType _type;

    [Header("Animation settings")]
    [SerializeField] private float _idleHeight = 0.1f;
    [SerializeField] private float _idleDuration = 1.5f;
    [SerializeField] private float _jumpPower = 1.5f;
    [SerializeField] private int _numJumps = 3;
    [SerializeField] private float _jumpDuration = 1.5f;
    [SerializeField] private float _maxJumpDistance = 1.5f;

    private int _value;
    private Sequence _appearAnimation;
    private Tween _idleAnimation;
    private BoxCollider2D _collider;

    public override FXType Type => _type;
    public int Value => _value;
    public AudioClip Sound => _sound;

    private void Awake()
    {
        _idleAnimation = _view.DOMoveY(_idleHeight, _idleDuration)
            .SetRelative(true)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false)
            .Pause();

        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _collider.enabled = false;
    }

    private void OnDisable()
    {
        _appearAnimation?.Kill();
        _idleAnimation.Pause();
    }

    public override void Play(Vector2 point)
    {
        gameObject.SetActive(true);

        transform.DOKill();
        _idleAnimation.Pause();

        transform.position = point;
        _view.localScale = Vector3.zero;

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(MinJumpDistance, _maxJumpDistance);
        Vector3 targetPos = point + randomDir * randomDistance;

        _appearAnimation?.Kill();

        _appearAnimation = DOTween.Sequence();

        _appearAnimation
            .Append(_view.DOScale(1f, 0.25f).From(0f)
                .SetEase(Ease.OutBack))
            .Join(transform.DOJump(targetPos, _jumpPower, _numJumps, _jumpDuration)
                .SetEase(Ease.OutBounce))
            .OnComplete(Moving)
            .Play();
    }

    public void Collect()
    {
        ReturnToPool?.Invoke(this);
    }

    public void SetReward(int value)
    {
        _value = value;
    }

    private void Moving()
    {
        _collider.enabled = true;
        _idleAnimation.Restart();
    }
}
