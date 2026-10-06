using UnityEngine;

// Libby/Jordan Script \\
public class EnemyState
{
    // base for all enemy states \\

    protected EnemyBase enemyBase;
    protected EnemyStateMachine enemySM;

    public EnemyState (EnemyBase enemyBase, EnemyStateMachine enemySM)
    {
        this.enemyBase = enemyBase;
        this.enemySM = enemySM;
    }


    public virtual void EnterState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void ExitState() { }
    public virtual void AnimationTriggerEvent(EnemyBase.AnimationTriggers trigger) { }
}
