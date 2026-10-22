using UnityEngine;

// Libby/Jordan Script \\
public class EnemyChaseState : EnemyState
{
    // called as soon as instance of script is created
    public EnemyChaseState(EnemyBase enemyBase, EnemyStateMachine enemySM) : base(enemyBase, enemySM)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.enemyChaseBaseInstance.DoEnterLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.enemyChaseBaseInstance.DoUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.enemyChaseBaseInstance.DoPhysicsUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.enemyChaseBaseInstance.DoExitLogic();
    }
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggers trigger)
    {
        base.AnimationTriggerEvent(trigger);
        enemyBase.enemyChaseBaseInstance.DoAnimationTriggerLogic(trigger);
    }
}
