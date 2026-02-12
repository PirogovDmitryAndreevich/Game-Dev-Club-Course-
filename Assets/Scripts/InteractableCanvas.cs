using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _keyF;

    private void Awake()
    {
        _keyF.SetActive(false);
    }

    public void ShowKeyF() => Show(_keyF);

    public void HideKeyF() => Hide(_keyF);

    private void Show(GameObject key) =>
            key.SetActive(true);

    private void Hide(GameObject key) =>
            key.SetActive(false);

}
