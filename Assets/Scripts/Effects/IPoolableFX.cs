using UnityEngine;

public interface IPoolableFX
{
    FXType Type { get; }
    void Play(Vector2 position);
    void ReturnToPool();
}
