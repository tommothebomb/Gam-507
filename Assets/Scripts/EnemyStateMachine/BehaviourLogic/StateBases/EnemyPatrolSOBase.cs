using UnityEngine;

// Libby/Jordan Script \\
public class EnemyPatrolSOBase : ScriptableObject
{
    protected EnemyBase enemyBase;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform player;


    public virtual void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemyBase = enemyBase;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoUpdateLogic()
    {
        if (enemyBase.IsAggroed)
        {
            enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoExitLogic() { }
    public virtual void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger) { }
}
