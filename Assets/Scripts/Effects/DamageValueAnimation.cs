using TMPro;
using UnityEngine;

public class DamageValueAnimation : FXBase
{
    private static readonly Color CritColor = Color.red;
    private static readonly Color NormalColor = Color.white;

    [SerializeField] private TMP_Text _value;
    [SerializeField] private float moveDuration = 0.4f;
    [SerializeField] private float stayDuration = 0.3f;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float moveY = 1.2f;
    [SerializeField] private float randomX = 0.5f;
    [SerializeField] private float scaleMultiplier = 1.3f;

    public override FXType Type => FXType.DamageNumber;

    private Transform _tr;

    private bool _isPlaying;
    private float _time;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private float TotalTime => moveDuration + stayDuration + fadeDuration;

    private void Awake()
    {
        _tr = transform;
        enabled = false; // Update выключен по умолчанию
    }

    public void Play(Vector2 position, int damage, bool isCrit = false)
    {
        _isPlaying = true;
        _time = 0f;

        _startPos = position;
        _endPos = position + new Vector2(
            Random.Range(-randomX, randomX),
            moveY
        );

        _tr.position = position;
        _tr.localScale = Vector3.one;

        _value.text = damage.ToString();
        _value.color = isCrit ? CritColor : NormalColor;
        _value.alpha = 1f;

        enabled = true; // включаем Update
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _time += Time.unscaledDeltaTime;

        // === MOVE + SCALE ===
        if (_time <= moveDuration)
        {
            float t = _time / moveDuration;

            _tr.position = Vector2.Lerp(_startPos, _endPos, EaseOutCubic(t));
            _tr.localScale = Vector3.one *
                Mathf.Lerp(1f, scaleMultiplier, EaseOutBack(t));
            return;
        }

        // === STAY ===
        if (_time <= moveDuration + stayDuration)
            return;

        // === FADE ===
        float fadeTime = _time - moveDuration - stayDuration;
        if (fadeTime <= fadeDuration)
        {
            float t = fadeTime / fadeDuration;
            _value.alpha = Mathf.Lerp(1f, 0f, t);
            return;
        }

        // === END ===
        Finish();
    }

    private void Finish()
    {
        _isPlaying = false;
        enabled = false;

        _value.alpha = 1f;
        _tr.localScale = Vector3.one;

        ReturnToPool();
    }

    // ===== EASING =====

    private static float EaseOutCubic(float t)
    {
        t = Mathf.Clamp01(t);
        return 1f - Mathf.Pow(1f - t, 3f);
    }

    private static float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        t -= 1f;
        return 1f + c3 * t * t * t + c1 * t * t;
    }

    public override void Play(Vector2 point)
    {
        throw new System.NotImplementedException();
    }
}
