using UnityEngine;


public class Key : Interactable, IInteractable
{
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private AudioClip _sound;

    private Color _color = Color.white;

    public AudioClip KeySound => _sound;
    public Sprite Icon => _icon.sprite;
    public Color SpriteColor 
    { 
        get => _color;
        set
        { 
            if (_color != value)
            {
                _color = value;
                _icon.color = _color;
            }
        } 
    }

    private void OnEnable()
    {
        _icon.color = SpriteColor;
        Moving();
    }

    public void Interact()
    {
        Destroy(gameObject);
    }
}
