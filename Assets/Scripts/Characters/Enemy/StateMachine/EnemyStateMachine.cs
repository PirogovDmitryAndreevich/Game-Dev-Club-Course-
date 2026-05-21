using System;
using System.Collections.Generic;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Enemy enemy)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this, enemy)},
            {typeof(IdleState), new IdleState(this, enemy)},
            {typeof(FollowState), new FollowState(this,enemy)},
            {typeof(AttackState), new AttackState(this, enemy)}
        };

        ChangeState<PatrolState>();
    }
}