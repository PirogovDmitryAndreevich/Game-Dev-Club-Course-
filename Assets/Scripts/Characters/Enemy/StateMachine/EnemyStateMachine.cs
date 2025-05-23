using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyDirectionOfView view, WayPoint[] wayPoints, EnemyAnimator animator,
                             float maxSqrDistance, Transform transform, float waitTime, float tryFindTime)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this,fliper,mover,view,wayPoints,maxSqrDistance, transform, animator) },
            {typeof(IdleState), new IdleState(this, view, waitTime) },
            {typeof(FollowState), new FollowState(this,animator, fliper, mover, view, tryFindTime) }
        };

        ChangeState<PatrolState>();
    }
}





