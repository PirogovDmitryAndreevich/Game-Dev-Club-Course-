using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyDirectionOfView view, WayPoint[] wayPoints,
        CharacterAnimator animator, float maxSqrDistance, Transform transform, float waitTime,
                             float tryFindTime, Attacker attacker, EnemyAI enemyAI, EnemySounds sounds)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this,enemyAI,fliper,mover,view,wayPoints,maxSqrDistance,
                                                    transform, animator, attacker.SqrAttackDistance, sounds) },
            {typeof(IdleState), new IdleState(this, view, animator, waitTime, attacker.SqrAttackDistance) },
            {typeof(FollowState), new FollowState(this,enemyAI,animator, fliper, mover, view, tryFindTime,
                                                    attacker.SqrAttackDistance, sounds) },
            {typeof(AttackState), new AttackState(this, animator, attacker,fliper ,view, attacker.CurrentDelay) }
        };

        ChangeState<PatrolState>();
    }
}





