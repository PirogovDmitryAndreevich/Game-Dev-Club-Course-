using UnityEngine;

class LostTargetTransition : Transition
{
    EnemyDirectionOfView _view;
    private float _endFindTime;
    private float _tryFindTime = 1f;

    public LostTargetTransition(StateMachine stateMachine, EnemyDirectionOfView view, float tryFindTime) : base(stateMachine)
    {
        _tryFindTime = tryFindTime;
        _view = view;
    }

    public override bool IsNeedTransit()
    {
        if (_view.TrySeeTarget(out Transform _))
        {
            _endFindTime = Time.time + _tryFindTime;
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