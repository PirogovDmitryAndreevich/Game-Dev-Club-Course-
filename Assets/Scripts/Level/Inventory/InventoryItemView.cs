using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private Image _image;

    public IInventoryObject Item {  get; private set; }

    internal void Initialize(IInventoryObject item)
    {
        Item = item;
        _image.sprite = item.Icon;
        _image.color = item.SpriteColor;
    }
}
