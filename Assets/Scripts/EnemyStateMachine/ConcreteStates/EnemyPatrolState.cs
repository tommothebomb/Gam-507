using UnityEngine;

// Libby/Jordan Script \\
public class EnemyPatrolState : EnemyState
{
    // called as soon as instance of script is created
    public EnemyPatrolState(EnemyBase enemyBase, EnemyStateMachine enemySM) : base(enemyBase, enemySM)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (enemyBase.IsAggroed)
        {
            enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggers trigger)
    {
        base.AnimationTriggerEvent(trigger);
    }
}
