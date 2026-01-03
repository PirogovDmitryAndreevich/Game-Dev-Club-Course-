using UnityEngine;

class AttackState : State
{
    private EnemyAttacker _attacker;
    private EnemyAnimator _animator;
    private EnemyDirectionOfView _vision;
    private LostTargetTransition _lostTargetTransition;
    private Transform _target;
    private Fliper _fliper;

    public AttackState(StateMachine stateMachine, EnemyAnimator animator, EnemyAttacker attacker,
        Fliper flipper, EnemyDirectionOfView vision, float tryFindTime) : base(stateMachine)
    {
        _attacker = attacker;
        _animator = animator;
        _fliper = flipper;
        _vision = vision;

        _lostTargetTransition = new LostTargetTransition(stateMachine, vision, tryFindTime);

        Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine,vision,vision.transform, _attacker.SqrAttackDistance),
                _lostTargetTransition
            };
    }

    public override void Enter()
    {
        _vision.TrySeeTarget(out _target);
        _lostTargetTransition.IsNeedTransit();
    }

    public override void Update()
    {
        if (_attacker.IsAttack == false)
            _fliper.LookAtTarget(_target.position);

        if (_attacker.CanAttack)
        {
            _attacker.StartAttack();
            _animator.SetAttackTrigger();
        }
    }

    public override void TryTransit()
    {
        if (_attacker.IsAttack == false)
            base.TryTransit();
    }
}





