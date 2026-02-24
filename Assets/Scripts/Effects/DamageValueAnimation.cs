using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageValueAnimation : FXBase
{
    private static readonly Color CritColor = Color.red;
    private static readonly Color NormalColor = Color.white;

    [SerializeField] private TMP_Text _value;

    [Header("Timing")]
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float fadeDelay = 0.2f;
    [SerializeField] private float fadeDuration = 0.25f;

    [Header("Motion")]
    [SerializeField] private float moveY = 1.5f;
    [SerializeField] private float randomX = 0.6f;
    [SerializeField] private float startScale = 0.6f;
    [SerializeField] private float punchScale = 0.4f;

    private Transform _transform;
    private Sequence _sequence;

    public override FXType Type => FXType.DamageNumber;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDisable()
    {
        KillSequence();
    }

    public override void Play(Vector2 point)
    {
        throw new System.NotImplementedException();
    }

    public void Play(Vector2 position, int damage, bool isCrit = false)
    {
        KillSequence(true);

        gameObject.SetActive(true);

        _transform.position = position;
        _transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));
        _transform.localScale = Vector3.one * 0.2f;

        _value.text = damage.ToString();
        _value.color = isCrit ? CritColor : NormalColor;
        _value.alpha = 1f;

        float sideOffset = Random.Range(-randomX, randomX);

        Vector3 peakPos = position + new Vector2(sideOffset, moveY * 0.6f);
        Vector3 endPos = position + new Vector2(sideOffset * 1.5f, moveY);

        _sequence = DOTween.Sequence();
        _sequence.SetUpdate(true);

        _sequence.Append(
            _transform.DOScale(1.4f, 0.15f)
                .SetEase(Ease.OutBack)
        );

        _sequence.Append(
            _transform.DOScale(1f, 0.1f)
                .SetEase(Ease.OutQuad)
        );

        _sequence.Join(
            _transform.DOMove(peakPos, moveDuration * 0.5f)
                .SetEase(Ease.OutQuad)
        );

        _sequence.Append(
            _transform.DOMove(endPos, moveDuration * 0.5f)
                .SetEase(Ease.InQuad)
        );

        _sequence.Join(
            _transform.DORotate(new Vector3(0, 0, Random.Range(-25f, 25f)), moveDuration)
                .SetEase(Ease.OutQuad)
        );

        _sequence.Insert(moveDuration * 0.6f,
            _value.DOFade(0f, fadeDuration)
                  .SetEase(Ease.InQuad)
        );

        _sequence.Insert(moveDuration * 0.6f,
            _transform.DOScale(0.6f, fadeDuration)
                      .SetEase(Ease.InBack)
        );

        _sequence.OnComplete(Finish);
        _sequence.Play();
    }

    private void Finish()
    {
        KillSequence(false);

        _value.alpha = 1f;
        _transform.localScale = Vector3.one;

        ReturnToPool?.Invoke(this);
    }

    private void KillSequence(bool complete = false)
    {
        if (_sequence != null)
        {
            if (_sequence.IsActive())
                _sequence.Kill(complete);

            _sequence = null;
        }
    }
}
