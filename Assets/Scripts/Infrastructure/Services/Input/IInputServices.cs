using UnityEngine;

public interface IInputServices
{
    Vector2 Direction { get; }

    bool GetIsAttack();
    bool GetIsDash();
    bool GetIsInteract();
}