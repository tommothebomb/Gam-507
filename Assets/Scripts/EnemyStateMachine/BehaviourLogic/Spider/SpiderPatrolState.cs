using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderPatrolState", menuName = "Scriptable Objects/Patrol/SpiderPatrolState")]
public class SpiderPatrolState : EnemyPatrolSOBase
{
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
