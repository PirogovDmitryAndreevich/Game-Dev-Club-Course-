using UnityEngine;

class IdleState : State
{
    EnemyAnimator _animator;

    private float _endWaitTime;
    private float _waitTime;

    public IdleState(StateMachine stateMachine, EnemyDirectionOfView vision,EnemyAnimator animator,
        float waitTime, float sqrAttackDistance) : base(stateMachine)
    {
        _waitTime = waitTime;
        _animator = animator;

        Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine,vision,vision.transform, sqrAttackDistance),
                new EndIdleTransition(stateMachine, this)
            };
    }

    public bool isEndWait => _endWaitTime <= Time.time;

    public override void Enter()
    {
        _endWaitTime = Time.time + _waitTime;
        _animator.SetIsWalk(false);
    }

}





