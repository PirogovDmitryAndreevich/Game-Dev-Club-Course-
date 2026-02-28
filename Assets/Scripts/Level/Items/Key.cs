using UnityEngine;


public class Key : Interactable, IItem, IInventoryObject
{
    [SerializeField] private SpriteRenderer _icon;

    private Color _color = Color.white;

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

    public void Collect()
    {
        Destroy(gameObject);
    }

}
