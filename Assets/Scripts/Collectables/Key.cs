using UnityEngine;


public class Key : Interactable
{
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private AudioClip _sound;

    private AudioHandler _handler;
    private Inventory _inventory;

    public Color Color { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            Collect();
    }

    public void Construct(AudioHandler handler, Color color, Inventory inventory)
    {
        _handler = handler;
        _inventory = inventory;
        Color = color;
        _icon.color = color;
    }

    public void Collect()
    {
        _handler.PlaySound(_sound);
        _inventory.AddKey(this);
        Destroy(gameObject);
    }
}
