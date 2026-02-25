using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class ConditionView : MonoBehaviour
{
    [SerializeField] private TaskType _type;

    [SerializeField] private TMP_Text _counter;
    [SerializeField] private Image _icon;
    [SerializeField] private Color _reachedColor;

    [Header("Animation")]
    [SerializeField] private float reachScale = 1.15f;

    private Sequence _sequence;
    private Vector3 _baseScale;

    private Image _conditionBackground;
    private int _task;
    private int _currentValue;
    private bool _alreadyReached = false;

    public TaskType Type => _type;

    private void Awake()
    {
        _conditionBackground = GetComponent<Image>();
        _baseScale = transform.localScale;
    }

    public void SetCondition(int currentValue, int task)
    {
        _task = task;
        UpdateCount(currentValue);
    }

    public void UpdateCount(int value)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _currentValue = value;
        _counter.text = $"{value} / {_task}";

        _sequence.Append(
        _counter.transform
                .DOPunchScale(Vector3.one * 0.3f, 0.3f, 8, 0.5f));

        _sequence.Play();
    }

    public void Reached()
    {
        if (_alreadyReached)
            return;

        _alreadyReached = true;

        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _sequence.Append(
        _conditionBackground
            .DOColor(_reachedColor, 0.3f)
            .SetEase(Ease.OutQuad)
    );

        _sequence.Join(
            transform.DOScale(_baseScale * reachScale, 0.2f)
                     .SetEase(Ease.OutBack)
        );

        _sequence.Append(
            transform.DOScale(_baseScale, 0.15f)
                     .SetEase(Ease.InQuad)
        );

        _sequence.Join(
            _icon.transform
                 .DOPunchScale(Vector3.one * 0.4f, 0.4f, 10, 0.6f)
        );

        _sequence.Play();
    }
}
