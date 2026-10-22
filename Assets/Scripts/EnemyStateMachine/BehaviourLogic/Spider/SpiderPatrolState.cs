using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderPatrolState", menuName = "Scriptable Objects/Patrol/SpiderPatrolState")]
public class SpiderPatrolState : EnemyPatrolSOBase
{
    // spider enemies will be relatively stationary in the patrol state, staying hidden from the player

    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("in patrol state");
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
        // play idle animation
    }
}
