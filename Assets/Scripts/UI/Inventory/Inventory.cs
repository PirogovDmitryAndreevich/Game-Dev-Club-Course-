using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private IUIFactory _factory;
    private Dictionary<Key, ItemView> _keys;

    public void Construct(IUIFactory factory)
    {
        _factory = factory;
        _keys = new();
    }

    public void AddKey(Key key)
    {
        ItemView itemView = _factory.CreateUIKey(key.Color, transform);
        _keys.Add(key, itemView);
    }

    public void RemoveKey(Key key)
    {
        Destroy(_keys[key].gameObject);
        _keys.Remove(key);        
    }

    public bool Contains(Key key) =>
        _keys.ContainsKey(key);
}
