using UnityEngine;
using UnityEngine.EventSystems;

public class StandaloneInput : InputServices
{
    public override Vector2 Direction =>
        GetDirection();

    public override bool GetIsAttack() =>
        GetAttackAction();

    public override bool GetIsDash() =>
        Input.GetKeyDown(KeyCode.Space);

    public override bool GetIsInteract() =>
        Input.GetKeyDown(KeyCode.F);

    private Vector2 GetDirection()
    {
        float _movementX = Input.GetAxis(ConstantsData.InputAxis.HorizontalAxis);
        float _movementY = Input.GetAxis(ConstantsData.InputAxis.VerticalAxis);

        return new Vector2(_movementX, _movementY);
    }

    private bool GetAttackAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return false;
            else
                return true;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}