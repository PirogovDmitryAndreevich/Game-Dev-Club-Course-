using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HitFlash : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color _whiteColor = Color.white;
    [SerializeField] private Color _yellowColor = Color.yellow;

    [Header("Timings")]
    [SerializeField] private float _whiteDuration = 0.05f;
    [SerializeField] private float _yellowDuration = 0.07f;

    private SpriteRenderer _renderer;
    private MaterialPropertyBlock _mpb;

    private float _timer;
    private FlashPhase _phase = FlashPhase.None;

    private static readonly int FlashAmountId =
        Shader.PropertyToID("_FlashAmount");
    private static readonly int FlashColorId =
        Shader.PropertyToID("_FlashColor");

    private enum FlashPhase
    {
        None,
        White,
        Yellow
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _mpb = new MaterialPropertyBlock();
        ResetFlash();
    }

    private void OnEnable()
    {
        ResetFlash();
        _phase = FlashPhase.None;
    }

    private void OnDisable()
    {
        ResetFlash();
        _phase = FlashPhase.None;
    }

    public void Play()
    {
        // перезапуск всегда безопасен
        _phase = FlashPhase.White;
        _timer = _whiteDuration;
        SetFlash(_whiteColor, 1f);
    }

    private void Update()
    {
        if (_phase == FlashPhase.None)
            return;

        _timer -= Time.deltaTime;
        if (_timer > 0f)
            return;

        switch (_phase)
        {
            case FlashPhase.White:
                _phase = FlashPhase.Yellow;
                _timer = _yellowDuration;
                SetFlash(_yellowColor, 1f);
                break;

            case FlashPhase.Yellow:
                ResetFlash();
                _phase = FlashPhase.None;
                break;
        }
    }

    private void ResetFlash()
    {
        SetFlash(Color.white, 0f);
    }

    private void SetFlash(Color color, float amount)
    {
        _renderer.GetPropertyBlock(_mpb);
        _mpb.SetColor(FlashColorId, color);
        _mpb.SetFloat(FlashAmountId, amount);
        _renderer.SetPropertyBlock(_mpb);
    }
}
