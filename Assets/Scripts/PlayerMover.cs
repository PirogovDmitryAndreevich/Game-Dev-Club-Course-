using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const float SPEED_COIFICIENT = 50f;
    private const string HORIZONTAL_AXIS = "Horizontal";

    [SerializeField] private float _speedX = 1;

    private float _directly;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _directly = Input.GetAxis(HORIZONTAL_AXIS);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_speedX * _directly * SPEED_COIFICIENT * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }
}
