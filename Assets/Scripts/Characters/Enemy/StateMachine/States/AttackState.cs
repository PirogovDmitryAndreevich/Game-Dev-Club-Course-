using UnityEngine;

class AttackState : State
{
    private EnemyAttacker _attacker;
    private EnemyAnimator _animator;
    private EnemyDirectionOfView _vision;
    private Transform _target;
    private Fliper _fliper;

    public AttackState(StateMachine stateMachine, EnemyAnimator animator, EnemyAttacker attacker,
        Fliper flipper, EnemyDirectionOfView vision, float tryFindTime) : base(stateMachine)
    {
        _attacker = attacker;
        _animator = animator;
        _fliper = flipper;
        _vision = vision;

        Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine,vision,vision.transform, _attacker.SqrAttackDistance),
                new LostTargetTransition(stateMachine, vision, tryFindTime)
            };
    }

    public override void Enter()
    {
        Debug.Log("AttackState Enter");

        base.Enter();
        _vision.TrySeeTarget(out _target);

        Debug.Log($"AttackState _target = {_target}");
    }

    public override void Update()
    {
        if (_attacker.IsAttack == false)
            _fliper.LookAtTarget(_target.position);

        if (_attacker.CanAttack)
        {
            _attacker.Attack();
            _animator.SetAttackTrigger();
        }
    }

    public override void TryTransit()
    {
        if (_attacker.IsAttack == false)
        base.TryTransit();
    }
}





