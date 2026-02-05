using UnityEngine;

[RequireComponent(typeof(Fliper))]
public abstract class Attacker : MonoBehaviour
{
    [SerializeField] protected float _delay = 2f;
    [SerializeField] protected AttackBase _attack;
    [SerializeField] protected LayerMask _targetLayer;

    protected Fliper _fliper;
    protected CameraShake _camera;
    protected float _endWaitTime;

    protected float _offset => _attack.Offset;
    protected float _radius => _attack.Radius;
    public virtual bool CanAttack => _endWaitTime <= Time.time;
    public virtual bool IsAttack { get; protected set; }
    public float Delay => _delay;
    public float SqrAttackDistance => _offset * _offset;
    public abstract AttacksType type { get; }

    protected virtual void Awake()
    {
        _fliper = GetComponent<Fliper>();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), _radius);
    }

    public abstract void Attack();

    public virtual void StartAttack()
    {
        _endWaitTime = Time.time + _delay;
        IsAttack = true;
    }
    public virtual void StartAttack(CameraShake camera)
    {
        StartAttack();
        _camera = camera;
    }

    public virtual void OnAttackEndedEvent() => IsAttack = false;

    protected virtual Vector2 GetAttackOrigin()
    {
        float directionCoefficient = _fliper?.IsTernRight ?? true ? 1 : -1;
        float originX = transform.position.x + _offset * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }
}
