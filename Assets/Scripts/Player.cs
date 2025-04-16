using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(InputReader))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private InputReader _inputReader;
    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != null)
            _mover.Move(_inputReader.Direction);

        if (_inputReader.GetIsDash() && _inputReader.Direction != null)
            _mover.Dash(_inputReader.Direction);
        
    }
}
