using UnityEngine;

public class Bus : MonoBehaviour
{
    private const string StartMovingAnimation = "StartMove";

    [SerializeField] private Animator _animator;

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
