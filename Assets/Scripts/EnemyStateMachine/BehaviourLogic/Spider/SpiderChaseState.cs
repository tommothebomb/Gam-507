using Unity.VisualScripting;
using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderChaseState", menuName = "Scriptable Objects/Chase/SpiderChaseState")]
public class SpiderChaseState : EnemyChaseSOBase
{
    // keeps distance from player and tries to shoot them with a web
    // only gets close enough for melee attacks when player is webbed
    /*[Tooltip("make sure this is smaller than the aggro check trigger")]
    [SerializeField] float rangedAttackDist;
    [Tooltip("make sure this is smaller than the attack range check trigger")]
    [SerializeField] float meleeAttackDist;*/
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float webForce;
    float attackTimer;
    float destinationTimer;
    float timeBetweenDestinationSet;

    Vector3 dirToPlayer;
    float distToTravel;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemyBase.shootPoint.gameObject.SetActive(true);
        attackTimer = 0;
        destinationTimer = 0;
        FindNewDestination();
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        // follow/stalk player from a distance --> needs to follow at a distance but player also needs to be able to get close enough to attack so cant use navmeshagent stopping dist

        Debug.DrawRay(gameObject.transform.position, dirToPlayer * distToTravel, Color.red);
        destinationTimer += Time.deltaTime;
        if (destinationTimer >= timeBetweenDestinationSet) FindNewDestination();

        attackTimer += Time.deltaTime;
        if (attackTimer >= timeBetweenAttacks)
        {
            // play shoot animation, pause movement during this to allow player time to attack
            enemyBase.agent.SetDestination(gameObject.transform.position);
            GameObject web = enemyBase.pooler.SpawnFromPool("webProjectile", enemyBase.shootPoint.position, Quaternion.identity);
            WebProjectileLogic logic = web.GetComponent<WebProjectileLogic>();
            logic.enemyBase = enemyBase;
            web.GetComponent<Rigidbody>().AddForce(enemyBase.shootPoint.forward * webForce, ForceMode.Impulse); // may swap this to constant linearVel in projectile behaviour later

            attackTimer = 0;
        }

        // check states and set back if needed
        if (!enemyBase.IsAggroable || !enemyBase.IsAggroed) enemyBase.stateMachine.ChangeState(enemyBase.patrolState);
    }

    void FindNewDestination()
    {
        // find closest point to player around 10 unit circle, set as destination
        // need to set back to local variables later
            dirToPlayer = Vector3.Normalize(enemyBase.player.transform.position - gameObject.transform.position);
        Debug.Log(dirToPlayer);
        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, enemyBase.player.transform.position);
            distToTravel = distanceFromPlayer - 8;
        Debug.Log(distToTravel);
        enemyBase.agent.SetDestination(dirToPlayer * distToTravel);

        Debug.DrawRay(gameObject.transform.position, dirToPlayer * distToTravel, Color.red);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = dirToPlayer * distToTravel;

        // wait for time from destination set before swapping to new
        destinationTimer = 0;
        timeBetweenDestinationSet = distanceFromPlayer / 10;
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemyBase.shootPoint.gameObject.SetActive(false);
    }
    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        base.DoAnimationTriggerLogic(trigger);
    }
}
