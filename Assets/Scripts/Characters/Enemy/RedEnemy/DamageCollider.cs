using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageCollider : MonoBehaviour
{
    public event Action<Player, Vector2> Hit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))        
            Hit?.Invoke(player, collider.ClosestPoint(transform.position));        
    }
}