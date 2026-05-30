using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BusStop : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _activatedSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _busSpawnPoint;
    [SerializeField] private Transform _busEndedPoint;

    private MaterialPropertyBlock _materialPropertyBlock;
    private AudioHandler _handler;

    public event Action Interacted;

    public Transform BusSpawnPoint => _busSpawnPoint;
    public Transform BusEndedPoint => _busEndedPoint;
    public bool CanInteract { get; private set; } = false;

    private void Awake()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))        
            HighlightOn();        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))        
            HighlightOff();        
    }

    public void Construct(AudioHandler handler)
    {
        _handler = handler;
    }

    public void Interact()
    {
        if (!CanInteract)
            return;

        _spriteRenderer.sprite = _activatedSprite;
        Interacted?.Invoke();
    }

    public void EnableInteract() => 
        CanInteract = true;

    public void HighlightOn()
    {
        if (!CanInteract)
            return;

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
