using Unity.VisualScripting;
using UnityEngine;

class AttackState : State
{
    private Attacker _attacker;
    private CharacterAnimator _animator;
    private EnemyDirectionOfView _vision;
    private LostTargetTransition _lostTargetTransition;
    private Transform _target;
    private Fliper _fliper;

    public AttackState(StateMachine stateMachine, CharacterAnimator animator, Attacker attacker,
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

            if (_attacker.type != AttacksType.RedEnemyAttack)
            {
                _animator.SetEnemyAttackTrigger();
            }
        }
    }

    public override void TryTransit()
    {
        if (_attacker.IsAttack == false)
            base.TryTransit();
    }
}





