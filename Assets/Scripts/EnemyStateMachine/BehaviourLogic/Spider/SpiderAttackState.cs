using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderAttackState", menuName = "Scriptable Objects/Attack/SpiderAttackState")]
public class SpiderAttackState : EnemyAttackSOBase
{
    /*[Tooltip("make sure this is smaller than the aggro check trigger")]
    [SerializeField] float rangedAttackDist;
    [Tooltip("make sure this is smaller than the attack range check trigger")]
    [SerializeField] float meleeAttackDist;*/


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("in attack state");
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        // attack player
        // shed web trapping them
        // move back
        // swap to chase state again
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
