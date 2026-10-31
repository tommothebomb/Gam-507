using UnityEngine;

// Libby/Jordan Script \\
public class EnemyAttackSOBase : ScriptableObject
{
    protected EnemyBase enemyBase;
    protected Transform transform;
    protected GameObject gameObject;


    public virtual void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemyBase = enemyBase;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoUpdateLogic() { }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoExitLogic() { }
    public virtual void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger) { }
}
