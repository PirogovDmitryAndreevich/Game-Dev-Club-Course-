using UnityEngine;

[RequireComponent(typeof(Fliper))]
public abstract class Attacker : MonoBehaviour
{
    [SerializeField] protected float Delay = 2f;
    [SerializeField] protected AttackBase AttackType;
    [SerializeField] protected LayerMask TargetLayer;

    protected Fliper Fliper;
    protected CameraShake Camera;
    protected float EndWaitTime;

    public virtual bool CanAttack => EndWaitTime <= Time.time;
    public virtual bool IsAttack { get; protected set; }
    public float CurrentDelay => Delay;
    public float SqrAttackDistance => Offset * Offset;
    public abstract AttacksType type { get; }
    protected float Offset => AttackType.Offset;
    protected float Radius => AttackType.Radius;

    private void Awake() =>
        AttackAwake();

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), Radius);
    }

    public abstract void Attack();

    public virtual void StartAttack()
    {
        EndWaitTime = Time.time + Delay;
        IsAttack = true;
    }
    public virtual void StartAttack(CameraShake camera)
    {
        StartAttack();
        Camera = camera;
    }

    public virtual void OnAttackEndedEvent() => IsAttack = false;

    protected virtual void AttackAwake()
    {
        Fliper = GetComponent<Fliper>();
    }

    protected virtual Vector2 GetAttackOrigin()
    {
        float directionCoefficient = Fliper?.IsTernRight ?? true ? 1 : -1;
        float originX = transform.position.x + Offset * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }
}
