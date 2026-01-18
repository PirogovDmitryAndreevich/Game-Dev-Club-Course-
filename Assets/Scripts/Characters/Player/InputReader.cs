using UnityEngine;
using UnityEngine.EventSystems;

public class InputReader : MonoBehaviour
{
    private bool _isAttack;
    private float _movementX;
    private float _movementY;
    private bool _isDash;
    private bool _isInteract;

    public Vector2 Direction { get; private set;}

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        _movementX = Input.GetAxis(ConstantsData.InputAxis.HorizontalAxis);
        _movementY = Input.GetAxis(ConstantsData.InputAxis.VerticalAxis);

        Direction = new Vector2(_movementX, _movementY);

        if (Input.GetKeyDown(KeyCode.Space))        
            _isDash = true;        

        if (Input.GetKeyDown(KeyCode.F))
            _isInteract = true;

        if (Input.GetMouseButtonDown(0))
            _isAttack = true;
    }

    public bool GetIsDash() => GetBoolIsTrigger(ref _isDash);

    public bool GetIsInteract() => GetBoolIsTrigger(ref _isInteract);

    public bool GetIsAttack() => GetBoolIsTrigger(ref _isAttack);

    private bool GetBoolIsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
