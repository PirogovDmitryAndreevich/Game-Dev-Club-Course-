using System;
using UnityEngine;

[RequireComponent(typeof(Fliper))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private float _offset;
    [SerializeField] private LayerMask _targetLayer;

    private Fliper _fliper;
    private float _endWaitTime;
    private float _endAttackTime;

    public float SqrAttackDistance => _offset * _offset;

    public bool CanAttack => _endWaitTime <= Time.time;

    public bool IsAttack => _endAttackTime > Time.time;

    private void Start()
    {
        _fliper = GetComponent<Fliper>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), _radius);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(GetAttackOrigin(), _radius, _targetLayer);

        _endWaitTime = Time.time + _delay;
        _endAttackTime = Time.time + 1;

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage);
        }
    }

    private Vector2 GetAttackOrigin()
    {
        float directionCoefficient = _fliper?.IsTernRight ?? true ? 1 : -1;
        float originX = transform.position.x + _offset * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }

    internal bool TrySeeTarget(out object target)
    {
        throw new NotImplementedException();
    }
}
