using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const float SPEED_COIFICIENT = 50f;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private float _speed = 3f;
    private float _movementX;
    private float _movementY;
    private bool _isDash;
    private float _dash = 9.5f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementX = Input.GetAxis(HORIZONTAL_AXIS);
        _movementY = Input.GetAxis(VERTICAL_AXIS);

        if (!_isDash && Input.GetKeyDown(KeyCode.Space))
        {
            _speed *= _dash;
            _isDash = true;
        }
        if (_isDash && Input.GetKeyUp(KeyCode.Space))
        {
            _speed /= _dash;
            _isDash = false;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_speed * _movementX * SPEED_COIFICIENT * Time.fixedDeltaTime, _speed * _movementY * SPEED_COIFICIENT * Time.fixedDeltaTime);
    }
}
