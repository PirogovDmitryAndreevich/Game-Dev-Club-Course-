using System;
using UnityEngine;

[RequireComponent(typeof(Fliper))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private float _offset;
    [SerializeField] private LayerMask _targetLayer;

    private Fliper _fliper;
    private float _endWaitTime;

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
        Vector2 origin = GetAttackOrigin();

        Collider2D hit = Physics2D.OverlapCircle(origin, _radius, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Enemy enemy))
        {
            Vector2 pushDirection = _fliper.IsTernRight
                                        ? Vector2.right
                                            : Vector2.left;

            enemy.ApplyDamage(_damage, hit.ClosestPoint(origin), pushDirection);
        }
    }

    public void StartAttack()
    {
        _endWaitTime = Time.time + _delay;
        IsAttack = true;
    }

    public void OnAttackEndedEvent() => IsAttack = false;

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
