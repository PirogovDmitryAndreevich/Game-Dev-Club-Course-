using UnityEngine;

[RequireComponent(typeof(EnemySounds))]
public class PinkEnemyAttacker : Attacker
{
    [SerializeField] private float _offsetDamage = 1f;

    public override AttacksType type => _attack.Type;
    private EnemySounds _sound;

    protected override void Awake()
    {
        _sound = GetComponent<EnemySounds>();
        base.Awake();
    }

    public override void Attack()
    {
        _sound.PlayAttackSound();

        Collider2D hit = Physics2D.OverlapCircle(transform.position, SqrAttackDistance, _targetLayer);

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            var bullet = FXPool.Instance.Get(FXType.Bullet) as Bullet;

            Vector2 point = player.transform.position;
            point.y -= _offsetDamage;
            bullet.Play(transform.position, point, _attack, _targetLayer);
        }       
    }
}
