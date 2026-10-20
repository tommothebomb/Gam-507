using UnityEngine;

// Libby/Jordan Script \\
public class EnemyAttackState : EnemyState
{
    // called as soon as instance of script is created
    public EnemyAttackState(EnemyBase enemyBase, EnemyStateMachine enemySM) : base(enemyBase, enemySM)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        enemyBase.enemyAttackBaseInstance.DoEnterLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemyBase.enemyAttackBaseInstance.DoUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemyBase.enemyAttackBaseInstance.DoPhysicsUpdateLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemyBase.enemyAttackBaseInstance.DoExitLogic();
    }
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggers trigger)
    {
        base.AnimationTriggerEvent(trigger);
        enemyBase.enemyAttackBaseInstance.DoAnimationTriggerLogic(trigger);
    }
}
