using System.Collections;
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
    private Coroutine _routine;

    private static readonly int FlashAmountId =
        Shader.PropertyToID("_FlashAmount");
    private static readonly int FlashColorId =
        Shader.PropertyToID("_FlashColor");

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _mpb = new MaterialPropertyBlock();
    }

    public void Play()
    {
        if (_routine != null)
            StopCoroutine(_routine);

        _routine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        SetFlash(_whiteColor, 1f);
        yield return new WaitForSeconds(_whiteDuration);

        SetFlash(_yellowColor, 1f);
        yield return new WaitForSeconds(_yellowDuration);

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
