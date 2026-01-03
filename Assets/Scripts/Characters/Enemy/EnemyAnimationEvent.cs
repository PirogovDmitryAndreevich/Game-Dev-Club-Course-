using System;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Action Attack;
    public Action EndAttack;

    /// Called in the animator
    public void InvokeAttackEvent() => Attack?.Invoke();

    public void InvokeEndAttackEvent() => EndAttack?.Invoke();
}
