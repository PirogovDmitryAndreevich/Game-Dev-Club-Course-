using UnityEngine;

class IdleState : State
{
    private float _endWaitTime;
    private float _waitTime;

    public IdleState(StateMachine stateMachine, EnemyDirectionOfView view, float waitTime) : base(stateMachine)
    {
        _waitTime = waitTime;

        Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine,view),
                new EndIdleTransition(stateMachine, this)
            };
    }

    public bool isEndWait => _endWaitTime <= Time.time;

    public override void Enter()
    {
        _endWaitTime = Time.time + _waitTime;
    }

}





