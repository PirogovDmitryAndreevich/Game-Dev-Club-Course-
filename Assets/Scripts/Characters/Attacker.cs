using UnityEngine;

[RequireComponent(typeof(Fliper))]
public abstract class Attacker : MonoBehaviour
{    
    [SerializeField] private Fliper _fliper;
    [SerializeField] protected float Delay = 2f;
    [SerializeField] protected AttackBase AttackType;
    [SerializeField] protected LayerMask TargetLayer;

    protected Fliper Fliper;
    protected float ColldownAttack;
    private bool _isAttacking = false;

    public bool CanAttack => ColldownAttack <= Time.time && !_isAttacking;
    public bool IsAttack { get; protected set; }
    public float CurrentDelay => Delay;
    public float SqrAttackDistance => Offset * Offset;
    public abstract AttacksType type { get; }
    protected float Offset => AttackType.Offset;
    protected float Radius => AttackType.Radius;

    public abstract void Attack();

    public void StartAttack()
    {
        IsAttack = true;
        Attack();
    }

    protected void EndedAttack()
    {
        IsAttack = false;
        ColldownAttack = Time.time + Delay;
    }

    protected virtual Vector2 GetAttackOrigin()
    {
        float directionCoefficient = Fliper?.IsTernRight ?? true ? 1 : -1;
        float originX = transform.position.x + Offset * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }
}
