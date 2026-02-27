using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BusStop : MonoBehaviour, IInteractable, IHighlight, IShowKey
{
    [SerializeField] private Sprite _activatedSprite;
    [SerializeField] private Transform _busSpawnPoint;
    [SerializeField] private Transform _busEndedPoint;

    private MaterialPropertyBlock _materialPropertyBlock;
    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;

    public event Action Interacted;

    public Transform BusSpawnPoint => _busSpawnPoint;
    public Transform BusEndedPoint => _busEndedPoint;

    public bool IsActivated { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void Interact()
    {
        _spriteRenderer.sprite = _activatedSprite;
        Interacted?.Invoke();
        IsActivated = true;
    }

    public void HighlightOn()
    {
        _spriteRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_Highlight", 1f);
        _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    public void HighlightOff()
    {
        _spriteRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_Highlight", 0f);
        _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
