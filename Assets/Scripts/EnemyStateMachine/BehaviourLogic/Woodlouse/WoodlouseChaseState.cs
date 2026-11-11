using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "WoodlouseChaseState", menuName = "Scriptable Objects/Chase/WoodlouseChaseState")]
public class WoodlouseChaseState : EnemyChaseSOBase
{
    [SerializeField] float shootInterval;
    [SerializeField] float distanceDestinationWillBeFromPlayer;
    [Tooltip("the higher this is the quicker the swaps will be")]
    [SerializeField] float timeBetweenDestinationModifier;
    [SerializeField] float minDistFromPlayerToFindNewDestination;
    [SerializeField] float minDistFromPlayerToShoot;
    float destinationTimer;
    float timeBetweenDestinationSet;
    float distanceFromPlayer;
    float shootTimer;
    bool isShooting;


    public override void DoEnterLogic()
    {
        enemyBase.shootPoint.gameObject.SetActive(true);
        destinationTimer = 0;
        shootTimer = 0;
        FindNewDestination();
    }

    public override void DoUpdateLogic()
    {
        // used in multiple equasions and only runs when this state is active so hoping this is excuseable
        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, enemyBase.player.transform.position);

        // follow/stalk player from a distance --> needs to follow at a distance but player also needs to be able to get close enough to attack so cant use navmeshagent stopping dist
        destinationTimer += Time.deltaTime;
        if (destinationTimer >= timeBetweenDestinationSet && !isShooting) FindNewDestination();

        // to prevent constant shooting 
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            // only shoot web when the agent is at its destination and the player is close enough
            if (enemyBase.agent.velocity.sqrMagnitude <= 0.2f && distanceFromPlayer <= minDistFromPlayerToShoot)
            {
                shootTimer = 0;
                isShooting = true;
                enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack1);
            }
        }

        // check states and set back if needed
        if (!enemyBase.IsAggroable || !enemyBase.IsAggroed) enemyBase.stateMachine.ChangeState(enemyBase.patrolState);
    }

    void FindNewDestination()
    {
        // find closest point to player around x unit circle, set as destination
        Vector3 dirToPlayer = Vector3.Normalize(enemyBase.player.transform.position - gameObject.transform.position);
        float distToTravel = distanceFromPlayer - distanceDestinationWillBeFromPlayer;
        Vector3 pointCloseToPlayerDestination = gameObject.transform.position + (dirToPlayer * distToTravel);
        enemyBase.agent.SetDestination(pointCloseToPlayerDestination);

        // wait for time from destination set before swapping to new
        destinationTimer = 0;
        timeBetweenDestinationSet = distanceFromPlayer / timeBetweenDestinationModifier;
    }

    void ShootWeb()
    {
        // play shoot animation, pause movement during this to allow player time to attack
        enemyBase.agent.SetDestination(gameObject.transform.position);
        GameObject web = enemyBase.pooler.SpawnFromPool("webProjectile", enemyBase.shootPoint.position, enemyBase.shootPoint.rotation);
        WebProjectileLogic logic = web.GetComponent<WebProjectileLogic>();
        logic.enemyBase = enemyBase;
    }

    public override void DoExitLogic()
    {
        enemyBase.shootPoint.gameObject.SetActive(false);
    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        if (trigger == EnemyBase.AnimationTriggers.Attack1) ShootWeb();
        else if (trigger == EnemyBase.AnimationTriggers.EndAttack1) { enemyBase.enemyAnimController.isAttacking = false; isShooting = false; }
    }
}
