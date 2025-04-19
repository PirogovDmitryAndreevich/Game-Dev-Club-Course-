using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const float SpeedDash = 5000f;
    private const float Speed = 5f;
    private const int TernShapePlayer = -1;

    private bool _isTernRight = true;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }    

    public void Dash(Vector2 direction)
    {
        Vector2 dashForce = new Vector2(direction.x , direction.y) * SpeedDash;
        _rigidbody.AddForce(dashForce, ForceMode2D.Force);        
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.x, direction.y) * Speed;

        if ((direction.x > 0 && _isTernRight == false)
           || (direction.x < 0 && _isTernRight == true))
            Flip();
    }
    private void Flip()
    {
        _isTernRight = !_isTernRight;
        Vector3 scale = transform.localScale;
        scale.x *= TernShapePlayer;
        transform.localScale = scale;
    }
}
