using UnityEngine;

public class InputReader : MonoBehaviour
{

    private float _movementX;
    private float _movementY;
    private bool _isDash;
    private bool _isInteract;

    public Vector2 Direction { get; private set;}

    private void Update()
    {
        _movementX = Input.GetAxis(ConstantsData.InputAxis.HorizontalAxis);
        _movementY = Input.GetAxis(ConstantsData.InputAxis.VerticalAxis);
        Direction = new Vector2(_movementX, _movementY);

        if (Input.GetKeyDown(KeyCode.Space))        
            _isDash = true;        

        if (Input.GetKeyDown(KeyCode.F))
            _isInteract = true;
    }

    public bool GetIsDash() => GetBoolIsTrigger(ref _isDash);

    public bool GetIsInteract() => GetBoolIsTrigger(ref _isInteract);

    private bool GetBoolIsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
