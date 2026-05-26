using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> InteractStarted;
    public event Action ShowingHindePressF;
    public event Action HideHindPressF;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var go = collision.gameObject;

        if (go.TryGetComponent(out IInteractable interactable))
            InteractStarted?.Invoke(interactable);

        if (go.TryGetComponent(out IShowKey showKey))
            if (!showKey.IsActivated)
                ShowingHindePressF?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var go = collision.gameObject;

        if (go.TryGetComponent(out IInteractable interactable))
            InteractStarted?.Invoke(null);

        if (go.TryGetComponent(out IShowKey showKey))
            if (!showKey.IsActivated)
                HideHindPressF?.Invoke();
    }
}
