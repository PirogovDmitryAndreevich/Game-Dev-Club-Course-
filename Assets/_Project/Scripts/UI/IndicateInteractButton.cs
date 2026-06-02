using UnityEngine;

public class IndicateInteractButton : MonoBehaviour
{
    public void Show(bool isShow) => 
        gameObject.SetActive(isShow);
}