using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemySounds))]
public class RangeEnemyAttacker : EnemyAttacker
{
    [SerializeField] private AnimationEvent _attackEvent;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private EnemyBulletSpawner _bulletSpawner;
    [SerializeField] private EnemySounds _sound;
    [SerializeField] private float _shootDuration = 1f;

    Collider2D[] _hits = new Collider2D[1];

    public override float AttackRange => Radius * Radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), AttackRange);
    }

    private void Start()
    {
        _attackEvent.AttackEnded += OnEndedAttackEvent;
        _attackEvent.DealDamage += OnDealDamage;
    }

    private void OnDestroy()
    {
        _attackEvent.AttackEnded -= OnEndedAttackEvent;
        _attackEvent.DealDamage -= OnDealDamage;
    }

    public override void Attack() => 
        _animator.SetEnemyAttackTrigger();

    private void OnDealDamage()
    {
        _sound.PlayAttackSound();

        Physics2D.OverlapCircleNonAlloc(transform.position, AttackRange, _hits, TargetLayer);

        Collider2D hit = _hits.FirstOrDefault();

        if (hit != null && hit.TryGetComponent(out Player player))
        {
            BombDamageArea area = _bulletSpawner.GetDamageArea();
            Bomb bullet = _bulletSpawner.GetBomb();

            Vector2 point = player.transform.position;

            bullet.Play(transform.position, point, _shootDuration);
            area.Play(point, _shootDuration, Data.Damage, Data.KnockbackForce, TargetLayer);
        }
    }

    private void OnEndedAttackEvent() => 
        EndedAttack();
}
