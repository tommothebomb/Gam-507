using UnityEngine;

// Libby/Jordan Script \\
public class EnemyDeathState : EnemyState
{
    // called as soon as instance of script is created
    public EnemyDeathState(EnemyBase enemyBase, EnemyStateMachine enemySM) : base(enemyBase, enemySM)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.enemyDeathBaseInstance.DoEnterLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.enemyDeathBaseInstance.DoUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.enemyDeathBaseInstance.DoPhysicsUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.enemyDeathBaseInstance.DoExitLogic();
    }
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggers trigger)
    {
        base.AnimationTriggerEvent(trigger);
        enemyBase.enemyDeathBaseInstance.DoAnimationTriggerLogic(trigger);
    }
}
