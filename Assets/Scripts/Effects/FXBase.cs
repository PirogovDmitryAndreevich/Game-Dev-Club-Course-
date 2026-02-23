using System;
using UnityEngine;

public abstract class FXBase : MonoBehaviour
{
    public bool IsActive;
    public abstract FXType Type { get; }
    public abstract void Play(Vector2 point);

    public Action<FXBase> ReturnToPool;
}
