using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(CharacterAnimator), typeof(Fliper))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected HitFlash _hitFlash;
    [SerializeField] protected AnimationEvent _animationEvent;
    [SerializeField] protected int _maxHealth = 100;

    protected CharacterAnimator _animator;
    protected Mover _mover;
    protected Fliper _fliper;
    protected Health _health;
    protected Attacker _attacker;

    public event Action OnCharacterDied;

    protected virtual void Awake()
    {
        _health = new Health(_maxHealth, 0, false);
        _mover = GetComponent<Mover>();
        _animator = GetComponent<CharacterAnimator>();
        _fliper = GetComponent<Fliper>();
        _animationEvent.DealDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEndedEvent;
        _health.OnDied += OnDied;
    }

    protected virtual void OnDestroy()
    {
        if (_animationEvent != null)
        {
            _animationEvent.DealDamage -= _attacker.Attack;
            _animationEvent.AttackEnded -= _attacker.OnAttackEndedEvent;
        }

        _health.OnDied -= OnDied;
    }

    protected abstract void FixedUpdate();

    public virtual void ApplyDamage(AttackBase damageInfo, Vector2 damageSource, Vector2 pushDirection)
    {
        _health.ApplyDamage(damageInfo.Damage);

        if (damageInfo.IsKnockback)
            _mover.Knockback(pushDirection, damageInfo.KnockbackForce);

        _hitFlash.Play();

        var punch = FXPool.Instance.Get(FXType.Punch);
        punch.Play(damageSource);
    }

    protected virtual void OnDied()
    {
        OnCharacterDied?.Invoke();
    }
}
