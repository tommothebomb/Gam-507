using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderChaseState", menuName = "Scriptable Objects/Chase/SpiderChaseState")]
public class SpiderChaseState : EnemyChaseSOBase
{
    // keeps distance from player and tries to shoot them with a web
    // only gets close enough for melee attacks when player is webbed
    [SerializeField] GameObject webProjectile;
    [Tooltip("make sure this is smaller than the aggro check trigger")]
    [SerializeField] float rangedAttackDist;
    [Tooltip("make sure this is smaller than the attack range check trigger")]
    [SerializeField] float meleeAttackDist;
    bool playerIsTrapped;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("in chase state");
        playerIsTrapped = false;
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        if(playerIsTrapped)
        {
            // move close enough to melee attack
            // melee attack will be set based on trigger
        }
        else
        {
            // follow/stalk player from a distance --> needs to follow at a distance but player also needs to be able to get close enough to attack so cant use navmeshagent stopping dist
            // periodically shoot webs to try to trap player
            // will shoot from pool, web itself will set if player is trapped or not
        }
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
