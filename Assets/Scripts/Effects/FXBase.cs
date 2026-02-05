using UnityEngine;

public abstract class FXBase : MonoBehaviour
{
    public bool IsActive;
    public abstract FXType Type { get; }
    public abstract void Play(Vector2 point);
    public virtual void ReturnToPool()
    {
        FXPool.Instance.Return(this);
    }
}
