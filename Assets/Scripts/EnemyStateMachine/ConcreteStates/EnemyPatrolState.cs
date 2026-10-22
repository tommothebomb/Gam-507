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
        enemyBase.enemyPatrolBaseInstance.DoEnterLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.enemyPatrolBaseInstance.DoUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.enemyPatrolBaseInstance.DoPhysicsUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.enemyPatrolBaseInstance.DoExitLogic();
    }
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggers trigger)
    {
        base.AnimationTriggerEvent(trigger);
        enemyBase.enemyPatrolBaseInstance.DoAnimationTriggerLogic(trigger);
    }
}
