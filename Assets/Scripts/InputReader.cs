using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private float _movementX;
    private float _movementY;
    private bool _isDash;

    public Vector2 Direction { get; private set;}

    private void Update()
    {
        _movementX = Input.GetAxis(HORIZONTAL_AXIS);
        _movementY = Input.GetAxis(VERTICAL_AXIS);
        Direction = new Vector2(_movementX, _movementY);

        if (Input.GetKeyDown(KeyCode.Space))        
            _isDash = true;        
    }

    public bool GetIsDash()
    {
        bool isDash = _isDash;
        _isDash = false;
        return isDash;
    }
}
