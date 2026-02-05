using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BusStop : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _activatedSprite;

    private MaterialPropertyBlock mpb;
    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    public bool IsActivated { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        mpb = new MaterialPropertyBlock();
    }

    public void Interact()
    {
        _spriteRenderer.sprite = _activatedSprite;
        IsActivated = true;
    }

    public void HighlightOn()
    {
        _spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Highlight", 1f);
        _spriteRenderer.SetPropertyBlock(mpb);
    }

    // Выключить подсветку
    public void HighlightOff()
    {
        _spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Highlight", 0f);
        _spriteRenderer.SetPropertyBlock(mpb);
    }
}
