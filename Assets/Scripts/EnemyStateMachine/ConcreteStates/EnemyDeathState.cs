using UnityEngine;

// Libby/Jordan Script \\
public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyBase enemyBase, EnemyStateMachine enemySM) : base(enemyBase, enemySM)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
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
