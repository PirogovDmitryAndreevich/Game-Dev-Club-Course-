using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> InteractStarted;
    public event Action OnShowKeyF;
    public event Action OnHideKeyF;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if (interactable is BusStop)
            {
                interactable.HighlightOn();
            }
        }

        if (collision.TryGetComponent(out IItem item))
        {
            item.CollisionEnter(_player);
        }

        InteractStarted?.Invoke(interactable);
        OnShowKeyF?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            InteractStarted?.Invoke(null);

            if (interactable is BusStop)
                interactable.HighlightOff();

            OnHideKeyF?.Invoke();
        }
    }
}
