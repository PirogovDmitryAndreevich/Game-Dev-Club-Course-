using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Construct(Color color)
    {
        _image.color = color;
    }
}