using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FinishDoor : MonoBehaviour, IInteractable
{
    private Animator _animator;
    private bool _isOpen;

    [SerializeField] private Rune[] runesForActivate;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (runesForActivate.All(i => i._isActivated == true) && _isOpen == false)
        {
            _animator.SetTrigger(ConstantsData.AnimatorParameters.IsOpen);
            _isOpen = true;
        }
        else
        {
            Debug.Log("Not all runes are activated");
        }
    }
}
