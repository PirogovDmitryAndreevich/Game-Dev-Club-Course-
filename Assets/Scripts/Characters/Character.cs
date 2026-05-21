using UnityEngine;

[RequireComponent(typeof(Mover), typeof(CharacterAnimator), typeof(Fliper))]
public abstract class Character : MonoBehaviour
{
    [Header("Character components")]
    [SerializeField] protected Mover Mover;
    [SerializeField] protected Fliper Fliper;
    [SerializeField] protected HitFlash HitFlash;
    [SerializeField] protected AnimationEvent AnimationEvent;
    [SerializeField] protected CharacterAnimator Animator;

    public virtual void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        if (damageInfo.IsKnockback)
            Mover.Knockback(pushDirection, damageInfo.KnockbackForce);

        HitFlash.Play();
    }
}
