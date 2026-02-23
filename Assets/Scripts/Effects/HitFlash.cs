using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HitFlash : MonoBehaviour
{
    private static readonly int FlashAmountId =
        Shader.PropertyToID("_FlashAmount");
    private static readonly int FlashColorId =
        Shader.PropertyToID("_FlashColor");

    [Header("Colors")]
    [SerializeField] private Color _whiteColor = Color.white;
    [SerializeField] private Color _yellowColor = Color.yellow;

    [Header("Timings")]
    [SerializeField] private float _whiteDuration = 0.05f;
    [SerializeField] private float _yellowDuration = 0.07f;

    private SpriteRenderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    private float _timer;
    private FlashPhase _phase = FlashPhase.None;    

    private enum FlashPhase
    {
        None,
        White,
        Yellow
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
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

    public void Play()
    {
        _phase = FlashPhase.White;
        _timer = _whiteDuration;
        SetFlash(_whiteColor, 1f);
    }

    private void ResetFlash()
    {
        SetFlash(Color.white, 0f);
    }

    private void SetFlash(Color color, float amount)
    {
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor(FlashColorId, color);
        _materialPropertyBlock.SetFloat(FlashAmountId, amount);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
