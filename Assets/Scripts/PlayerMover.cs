using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const float DASH = 5000f;
    private const float SPEED = 5f;

    private bool _isTernRight = true;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }    

    public void Dash(Vector2 direction)
    {
        Vector2 dashForce = new Vector2(DASH * direction.x, DASH * direction.y);
        _rigidbody.AddForce(dashForce, ForceMode2D.Force);        
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(SPEED * direction.x, SPEED * direction.y);

        if ((direction.x > 0 && _isTernRight == false) || (direction.x < 0 && _isTernRight == true))
            Flip();
    }
    private void Flip()
    {
        _isTernRight = !_isTernRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
