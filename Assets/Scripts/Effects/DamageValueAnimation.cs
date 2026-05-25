using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageValueAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;

    [Header("Timing")]
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float fadeDuration = 0.25f;

    [Header("Motion")]
    [SerializeField] private float moveY = 1.5f;
    [SerializeField] private float randomX = 0.6f;

    private Transform _transform;
    private Sequence _sequence;
    private IPoolService _pool;

    public void Construct(IPoolService pool)
    {
        _pool = pool;
        _transform = transform;
    }

    public void Play(Vector2 position, int damage)
    {
        gameObject.SetActive(true);

        KillSequence();
        _sequence = DOTween.Sequence();
        _sequence.SetUpdate(true);

        _transform.position = position;
        _transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));

        _value.text = damage.ToString();

        float sideOffset = Random.Range(-randomX, randomX);

        Vector3 peakPos = position + new Vector2(sideOffset, moveY * 0.6f);
        Vector3 endPos = position + new Vector2(sideOffset * 1.5f, moveY);        

        _sequence.Append(
            _transform.DOScale(1.4f, 0.15f).From(0f)
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
        KillSequence();

        _value.alpha = 1f;
        _transform.localScale = Vector3.one;

        _pool.Return(this);
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
