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

    public virtual void ApplyDamage(int damage, float knockbackForce, Vector2 damageSource, Vector2 pushDirection)
    {
        Mover.Knockback(pushDirection, knockbackForce);
        HitFlash.Play();
    }
}
