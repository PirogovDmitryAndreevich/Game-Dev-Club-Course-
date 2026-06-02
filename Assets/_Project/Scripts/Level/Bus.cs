using UnityEngine;

public class Bus : MonoBehaviour
{
    private const string StartMovingAnimation = "StartMove";

    [SerializeField] private Animator _animator;

    public Vector2 EndMovePoint {  get; private set; }

    public void Construct(Vector2 endMovePoint, bool isFromRight)
    {
        EndMovePoint = endMovePoint;
        SetFlip(isFromRight);
    }

    public void SetFlip(bool isFromRight)
    {
        Vector3 scale = transform.localScale;

        if (!isFromRight)
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    public void SetAnimation(bool isMove) =>
         _animator.SetBool(StartMovingAnimation, isMove);

}
