using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "DefaultDeathState", menuName = "Scriptable Objects/Death/DefaultDeathState")]
public class DefaultDeathState : EnemyDeathSOBase
{
    // deactivate colliders, play death animation
    // wait a few seconds then fade model to transparent
    // delete/deactivate model (depending on wether pooled or not)


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        base.DoAnimationTriggerLogic(trigger);
    }
}
