using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventoryItemView _itemPrefab;

    private List<InventoryItemView> _items = new();

    public void Add(IInventoryObject item)
    {
        InventoryItemView itemView = Instantiate(_itemPrefab, transform);
        itemView.Initialize(item);
        _items.Add(itemView);
    }

    public void Remove(IInventoryObject item)
    {
        foreach (InventoryItemView itemView in _items)
        {
            if(itemView.Item == item)
            {
                Destroy(itemView.gameObject);
                _items.Remove(itemView);
                return;
            }
        }
    }
}
