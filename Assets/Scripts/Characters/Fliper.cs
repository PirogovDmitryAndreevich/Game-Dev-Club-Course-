using UnityEngine;

public class Fliper : MonoBehaviour
{
    public bool IsTernRight { get; private set; } = true;

    public void LookAtTarget(Vector2 targetPosition)
    {
        if ((transform.position.x < targetPosition.x && IsTernRight == false)
       || (transform.position.x > targetPosition.x && IsTernRight == true))
        {
            IsTernRight = !IsTernRight;
            transform.Flip();
        }
    }

}
