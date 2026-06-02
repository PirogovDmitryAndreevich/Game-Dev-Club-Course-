using UnityEngine;

class AttackState : State
{
    private Enemy _enemy;
    private LostTargetTransition _lostTargetTransition;
    private Transform _target;

    public AttackState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _enemy = enemy;

        _lostTargetTransition = new LostTargetTransition(stateMachine, enemy);

        Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine,enemy),
                _lostTargetTransition
            };
    }

    public override void Enter()
    {
        _enemy.View.TrySeeTarget(out _target);
        _lostTargetTransition.IsNeedTransit();
    }

    public override void Update()
    {
        if (!_enemy.Attacker.IsAttack)
            _enemy.EnemyFliper.LookAtTarget(_target.position);

        if (_enemy.Attacker.CanAttack)        
            _enemy.Attacker.StartAttack();        
    }

    public override void TryTransit()
    {
        if (!_enemy.Attacker.IsAttack)
            base.TryTransit();
    }
}





