using UnityEngine;

public class MobileInput : InputServices
{
    private const string AttackButton = "Attack";
    private const string InteractButton = "Interact";
    private const string DashButton = "Dash";

    public override Vector2 Direction =>
        SimpleInputAxis();

    public override bool GetIsAttack() => 
        SimpleInput.GetButtonDown(AttackButton);

    public override bool GetIsDash() =>
        SimpleInput.GetButtonDown(DashButton);

    public override bool GetIsInteract() =>
        SimpleInput.GetButtonDown(InteractButton);

    private Vector2 SimpleInputAxis() =>
        new Vector2
        (
            SimpleInput.GetAxis(ConstantsData.InputAxis.HorizontalAxis),
            SimpleInput.GetAxis(ConstantsData.InputAxis.VerticalAxis)
        );
}