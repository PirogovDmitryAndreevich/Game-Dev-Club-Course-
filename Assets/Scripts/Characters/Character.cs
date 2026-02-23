using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(CharacterAnimator), typeof(Fliper))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected FXPool FXPool;
    [SerializeField] protected HitFlash HitFlash;
    [SerializeField] protected AnimationEvent AnimationEvent;
    [SerializeField] protected int MaxHealth = 100;

    protected CharacterAnimator Animator;
    protected Mover Mover;
    protected Fliper Fliper;
    protected Health Health;
    protected Attacker Attacker;

    public event Action CharacterDied;

    private void Awake() =>    
        CharacterAwake();    

    private void OnDestroy() =>    
        CharacterDestroy();

    private void FixedUpdate() =>
        CharacterFixUpdate();

    public virtual void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        Health.ApplyDamage(damageInfo.Damage);

        if (damageInfo.IsKnockback)
            Mover.Knockback(pushDirection, damageInfo.KnockbackForce);

        HitFlash.Play();

        var punch = FXPool.Get(FXType.Punch);
        punch.ReturnToPool += ReturnFXToPool;
        punch.Play(damageSource);
    }

    protected virtual void CharacterAwake()
    {
        Health = new Health(MaxHealth, 0, false);
        Mover = GetComponent<Mover>();
        Animator = GetComponent<CharacterAnimator>();
        Fliper = GetComponent<Fliper>();
        AnimationEvent.DealDamage += Attacker.Attack;
        AnimationEvent.AttackEnded += Attacker.OnAttackEndedEvent;
        Health.Died += OnDied;
    }

    protected virtual void CharacterDestroy()
    {
        if (AnimationEvent != null)
        {
            AnimationEvent.DealDamage -= Attacker.Attack;
            AnimationEvent.AttackEnded -= Attacker.OnAttackEndedEvent;
        }

        Health.Died -= OnDied;
    }

    protected abstract void CharacterFixUpdate();

    protected void ReturnFXToPool(FXBase fx)
    {
        fx.ReturnToPool -= ReturnFXToPool;
        FXPool.Return(fx);
    }

    protected virtual void OnDied()
    {
        CharacterDied?.Invoke();
    }
}
