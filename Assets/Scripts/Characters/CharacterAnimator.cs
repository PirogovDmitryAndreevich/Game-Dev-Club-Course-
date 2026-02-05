using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetIsWalk(bool isWalk)
    {
        _animator.SetBool(ConstantsData.AnimatorParameters.IsWalk, isWalk);
    }
    public void SetHitTrigger()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.Hit);
    }

    public void SetEnemyAttackTrigger()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsAttack);
    }

    public void SetEnemyAttackBool(bool isAttackEnded)
    {
        _animator.SetBool(ConstantsData.AnimatorParameters.IsAttackBool, isAttackEnded);
    }

    public void SetPlayerDashTrigger()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsDash);
    }

    public void SetDefaultPlayerAttackTrigger()
    {
        _animator.SetTrigger(ConstantsData.AnimatorParameters.IsAttack);
    }
}
