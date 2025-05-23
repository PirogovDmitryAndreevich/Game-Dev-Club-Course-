using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const float SpeedDash = 5000f;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _runSpeed = 6f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Dash(Vector2 direction)
    {
        Vector2 dashForce = new Vector2(direction.x, direction.y) * SpeedDash;
        _rigidbody.AddForce(dashForce, ForceMode2D.Force);
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.x, direction.y) * _speed;
    }

    public void Run(Transform target) => Move(target, _runSpeed);

    public void Walk(Transform target) => Move(target, _speed);

    private void Move(Transform target, float speed)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }
}
