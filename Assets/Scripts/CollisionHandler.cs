using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> InteractStarted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if (interactable is BusStop)
                InteractStarted?.Invoke(interactable);

            interactable.HighlightOn();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            InteractStarted?.Invoke(null);

            if (interactable is BusStop)
                interactable.HighlightOff();
        }
    }
}
