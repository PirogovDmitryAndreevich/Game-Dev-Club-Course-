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

    public float Delay => _delay;

    public float SqrAttackDistance => _offset * _offset;

    public bool CanAttack => _endWaitTime <= Time.time;

    public bool IsAttack { get; private set; }

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

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage);
        }
    }

    public void StartAttack()
    {
        _endWaitTime = Time.time + _delay;
        IsAttack = true;
    }
    public void OnEndAttackEvent() => IsAttack = false;

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
