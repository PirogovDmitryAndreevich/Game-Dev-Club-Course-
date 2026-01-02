using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Rune : MonoBehaviour, IInteractable
{
    private Animator _animator;

    public bool IsActivated { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsActivated);
        IsActivated = true;
    }
}
