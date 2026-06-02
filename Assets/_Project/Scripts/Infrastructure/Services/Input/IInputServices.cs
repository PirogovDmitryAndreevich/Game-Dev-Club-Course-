using UnityEngine;

public interface IInputServices : IService
{
    Vector2 Direction { get; }

    bool GetIsAttack();
    bool GetIsDash();
    bool GetIsInteract();
}