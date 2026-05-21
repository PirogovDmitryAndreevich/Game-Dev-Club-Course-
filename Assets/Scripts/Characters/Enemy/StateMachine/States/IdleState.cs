using UnityEngine;

class IdleState : State
{
    private Enemy _enemy;

    private float _endWaitTime;

    public IdleState(StateMachine stateMachine, Enemy enemy) : base(stateMachine)
    {
        _enemy = enemy;

        Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine,enemy),
                new EndIdleTransition(stateMachine, this)
            };
    }

    public bool isEndWait => _endWaitTime <= Time.time;

    public override void Enter()
    {
        _endWaitTime = Time.time + _enemy.StaticData.WaitTime;
        _enemy.EnemyAnimator.SetIsWalk(false);
    }

}





