using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void SetIsWalk (bool isWalk)
    {
        _animator.SetBool(ConstantsData.AnimatorParameters.IsWalk, isWalk);
    }
}
