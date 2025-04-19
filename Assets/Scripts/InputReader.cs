using UnityEngine;

public class InputReader : MonoBehaviour
{

    private float _movementX;
    private float _movementY;
    private bool _isDash;

    public Vector2 Direction { get; private set;}

    private void Update()
    {
        _movementX = Input.GetAxis(ConstantsData.InputAxis.HorizontalAxis);
        _movementY = Input.GetAxis(ConstantsData.InputAxis.VerticalAxis);
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
