using UnityEngine;

class LostTargetTransition : Transition
{
    private float _endFindTime;
    private Enemy _enemy;

    public LostTargetTransition(StateMachine stateMachine, Enemy enemy) : base(stateMachine) => 
        _enemy = enemy;

    public override bool IsNeedTransit()
    {
        if (_enemy.View.TrySeeTarget(out Transform _))
        {
            _endFindTime = Time.time + _enemy.StaticData.TryFindTime;
        }
        else if (_endFindTime < Time.time)
        {
            return true;
        }

        return false;
    }

    public override void Transit()
    {
        base.Transit();

        StateMachine.ChangeState<IdleState>();
    }
}