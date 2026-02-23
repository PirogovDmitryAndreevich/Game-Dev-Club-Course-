using UnityEngine;

public class InteractableCanvas : MonoBehaviour
{
    [SerializeField] private UrgePressF _urgePressF;

    private void Awake()
    {
        _urgePressF.gameObject.SetActive(false);
    }

    public void ShowKeyF() => Show(_urgePressF);

    public void HideKeyF() => Hide(_urgePressF);

    private void Show(HintBase hint) =>
            hint.gameObject.SetActive(true);

    private void Hide(HintBase hint) =>
            hint.gameObject.SetActive(false);

}
