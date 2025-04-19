using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(InputReader), typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private InputReader _inputReader;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        _animator.SetIsWalk(_inputReader.Direction.x != 0
                           || _inputReader.Direction.y != 0);

        if (_inputReader.Direction != null)
            _mover.Move(_inputReader.Direction);

        if (_inputReader.GetIsDash() && _inputReader.Direction != null)
            _mover.Dash(_inputReader.Direction);
        
        
    }
}
