using UnityEngine;


public class Key : Interactable, IItem, IInventoryObject
{
    [SerializeField] private SpriteRenderer _icon;

    public Sprite Icon => _icon.sprite;
    public Color ColorKey { get; set; }
    public Color SpriteColor => ColorKey;

    private void OnEnable()
    {
        _icon.color = ColorKey;
        Moving();
    }

    public void Collect()
    {
        Destroy(gameObject);
    }

}
