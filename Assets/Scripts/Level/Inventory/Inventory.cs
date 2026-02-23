using System;
using System.Collections.Generic;

public class Inventory 
{
    private List<IInventoryObject> _items;

    public Inventory()
    {
        _items = new();
    }

    public event Action<IInventoryObject> ItemAdded;
    public event Action<IInventoryObject> ItemRemoved;

    public void Add(IInventoryObject item)
    {
        _items.Add(item);
        ItemAdded?.Invoke(item);
    }

    public void Remove(IInventoryObject item)
    {
        _items.Remove(item);
        ItemRemoved?.Invoke(item);
    }

    public bool Contains(IInventoryObject item)
    {
        return _items.Contains(item);
    }
}
