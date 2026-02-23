using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Game/Attacks")]
public class AttackBase : ScriptableObject
{
    [SerializeField] private AttacksType _type;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _offset;
    [SerializeField] private bool _isKnockback;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private bool _isCrit;

    public Action<int> DamageChanged;

    public AttacksType Type => _type;

    public int Damage
    {
        get => _damage;
        set
        {
            if (_damage != value)
                _damage = value;

            DamageChanged?.Invoke(_damage);
        }
    }
    public float Radius => _radius;
    public float Offset => _offset;
    public bool IsKnockback => _isKnockback;
    public float KnockbackForce => _knockbackForce;
    public bool IsCrit => _isCrit;  

    public void SetDamage(int damage, AttacksType type)
    {
        if (_type == type)
            _damage = damage;
    }
}
