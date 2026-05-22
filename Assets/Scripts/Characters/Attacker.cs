using UnityEngine;

[RequireComponent(typeof(Fliper))]
public abstract class Attacker : MonoBehaviour
{
    [SerializeField] private Fliper _fliper;
    [SerializeField] protected LayerMask TargetLayer;

    private float _colldownAttack;

    public abstract float CooldownTime { get; }
    public abstract float Offset { get; }
    public abstract float Radius { get; }
    public bool CanAttack => _colldownAttack <= Time.time && !IsAttack;
    public float SqrAttackDistance => Offset * Offset;
    public bool IsAttack { get; protected set; } = false;
    protected Fliper Fliper => _fliper;

    public abstract void Attack();

    public void StartAttack()
    {
        IsAttack = true;
        Attack();
    }

    protected void EndedAttack()
    {
        IsAttack = false;
        _colldownAttack = Time.time + CooldownTime;
    }

    protected virtual Vector2 GetAttackOrigin()
    {
        float directionCoefficient = Fliper?.IsTernRight ?? true ? 1 : -1;
        float originX = transform.position.x + Offset * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }
}
