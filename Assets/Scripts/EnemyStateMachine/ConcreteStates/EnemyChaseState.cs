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
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (enemyBase.IsWithinAttackDist)
        {
            enemyBase.stateMachine.ChangeState(enemyBase.attackState);
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
