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
    [SerializeField] float timeBetweenAttacks;
    float attackTimer;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("in chase state");
        attackTimer = 0;
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        // follow/stalk player from a distance --> needs to follow at a distance but player also needs to be able to get close enough to attack so cant use navmeshagent stopping dist

        attackTimer += Time.deltaTime;
        if (attackTimer >= timeBetweenAttacks)
        {
            // play shoot animation, pause movement during this to allow player time to attack
            GameObject web = enemyBase.pooler.SpawnFromPool("webProjectile", gameObject.transform.position, Quaternion.identity);
            WebProjectileLogic logic = web.GetComponent<WebProjectileLogic>();
            logic.enemyBase = enemyBase;
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
