using UnityEngine;

public abstract class InputServices : IInputServices
{
    public abstract Vector2 Direction { get; }

    public abstract bool GetIsDash();

    public abstract bool GetIsInteract();

    public abstract bool GetIsAttack();
}