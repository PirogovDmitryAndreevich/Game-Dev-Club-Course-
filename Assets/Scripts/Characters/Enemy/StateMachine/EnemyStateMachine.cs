using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyDirectionOfView view, WayPoint[] wayPoints,
        EnemyAnimator animator, float maxSqrDistance, Transform transform, float waitTime,
                             float tryFindTime, EnemyAttacker attacker)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this,fliper,mover,view,wayPoints,maxSqrDistance,
                                                    transform, animator, attacker.SqrAttackDistance) },
            {typeof(IdleState), new IdleState(this, view, animator, waitTime, attacker.SqrAttackDistance) },
            {typeof(FollowState), new FollowState(this,animator, fliper, mover, view, tryFindTime,
                                                    attacker.SqrAttackDistance) },
            {typeof(AttackState), new AttackState(this, animator, attacker,fliper ,view, attacker.Delay) }
        };

        ChangeState<PatrolState>();
    }
}





