using UnityEngine;

public class Key : Interactable, IItem, IInventoryObject
{
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;

    private void OnEnable()
    {
        Moving();
    }

    public void Collect()
    {
        Destroy(gameObject);
    }

}
