using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : Attacker
{
    [Header("Player attacker")]
    [SerializeField] private AnimationEvent _attackEvent;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private int _superHitCount = 3;
    [SerializeField] private float _comboCooldown = 1.2f;

    private AttackBase _defaultAttack;
    private PlayerStaticData _staticData;
   
    private int _hitCounter;
    private float _lastHitTime;

    public override float CooldownTime => _staticData.CooldownTime;
    public override float Offset => _staticData.AttackOffset;
    public override float Radius => _staticData.AttackRadius;

    private List<AttackBase> _superAttacks = new();
    private Collider2D[] _hits = new Collider2D[16];

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetAttackOrigin(), Radius);
    }

    private void OnDestroy() => 
        _attackEvent.AttackEnded -= OnEndedAttackEvent;

    private void Start() => 
        _attackEvent.AttackEnded += OnEndedAttackEvent;

    public void Construct(PlayerStaticData data) => 
        _staticData = data;

    public override void Attack()
    {
        Vector2 origin = GetAttackOrigin();

        bool isSuperHit = RegisterHit();

        int countHits = Physics2D.OverlapCircleNonAlloc(origin, Radius, _hits, TargetLayer);

        /*AttackBase attack = isSuperHit
            ? _superAttacks[Random.Range(0, _superAttacks.Count)]
            : AttackType;*/

        if (isSuperHit)
        {
            _cameraShake.ShakeSuperPunch();
        }
        else
        {
            _cameraShake.ShakePunch();
        }

        for (int i =0; i < countHits; i++)
        {
            var hit = _hits[i];

            if (hit != null && hit.TryGetComponent(out Enemy enemy))
            {
                Vector2 pushDirection = Fliper.IsTernRight
                                        ? Vector2.right
                                        : Vector2.left;

               // enemy.ApplyDamage(attack, hit.ClosestPoint(origin), pushDirection);
            }
        }        
    }

    private void OnEndedAttackEvent() => 
        EndedAttack();

    private bool RegisterHit()
    {
        float time = Time.time;

        if (time - _lastHitTime > _comboCooldown)
        {
            _hitCounter = 0;
        }

        _lastHitTime = time;
        _hitCounter++;

        if (_hitCounter >= _superHitCount)
        {
            _hitCounter = 0;
            return true; 
        }

        return false;
    }
}