using UnityEngine;

[RequireComponent(typeof(Fliper))]
public abstract class EnemyAttacker : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _radius;
    [SerializeField] protected float _delay = 2f;
    [SerializeField] protected float _offset;
    [SerializeField] protected LayerMask _targetLayer;

    protected Fliper _fliper;
    private float _endWaitTime;

    public float Delay => _delay;

    public float SqrAttackDistance => _offset * _offset;

    public bool CanAttack => _endWaitTime <= Time.time;

    public bool IsAttack { get; private set; }

    protected virtual void Start()
    {
        _fliper = GetComponent<Fliper>();
    }

    protected virtual void OnDrawGizmos() { }

    public abstract void Attack();

    public void StartAttack()
    {
        _endWaitTime = Time.time + _delay;
        IsAttack = true;
    }
    public virtual void OnAttackEndedEvent() => IsAttack = false;

    protected virtual Vector2 GetAttackOrigin()
    {
        float directionCoefficient = _fliper?.IsTernRight ?? true ? 1 : -1;
        float originX = transform.position.x + _offset * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }
}
