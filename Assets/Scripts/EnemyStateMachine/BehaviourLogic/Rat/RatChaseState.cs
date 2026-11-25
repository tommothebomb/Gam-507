using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "RatChaseState", menuName = "Scriptable Objects/Chase/RatChaseState")]
public class RatChaseState : EnemyChaseSOBase
{
    // depending on health will have two different behaviours
    // if health is low, will move slower and retreat from player to a designated 'nest'
    // else will chase player
    LookAtPlayer thisLookAt;
    [SerializeField] float attackInterval;
    float attackTimer;
    bool canAttack;
    [SerializeField] float healtAmountToRetreat;
    [SerializeField] float hurtSpeed;
    bool isHurt;
    float destinationTimer;
    float timeBetweenDestinationSet = 0.4f;
    float distanceDestinationWillBeFromPlayer = 3.5f;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        thisLookAt = gameObject.GetComponent<LookAtPlayer>();
        isHurt = false;
    }

    public override void DoEnterLogic()
    {
        attackTimer = 0;
        canAttack = false;
        if (enemyBase.currentHealth <= healtAmountToRetreat) isHurt = true;
    }

    public override void DoUpdateLogic()
    {
        if (enemyBase.currentHealth <= healtAmountToRetreat && !isHurt) // to prevent running every frame when doesn't need to
        {
            Debug.Log("starting hurt");
            SetHurtLogic();
            Debug.Log("ran hurt");
        }
        else if (!isHurt) // normal state 
        {
            destinationTimer += Time.deltaTime;
            if (destinationTimer >= timeBetweenDestinationSet) FindNewDestination();
        }

        //Debug.Log(enemyBase.agent.destination + " " + enemyBase.agent.remainingDistance);
        if (enemyBase.agent.remainingDistance <= 0.5f && isHurt)
        {
            gameObject.SetActive(false);
            isHurt = false;
        }

        // check aggro and set back to patrol if needed
        if (!enemyBase.IsAggroable && !isHurt) enemyBase.stateMachine.ChangeState(enemyBase.patrolState);
        // if player within attack radius, isnt hurt and attack interval reached, swap to attack state
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval) canAttack = true;
        if (enemyBase.IsWithinAttackDist && !isHurt && canAttack) enemyBase.stateMachine.ChangeState(enemyBase.attackState);
    }

    void SetHurtLogic()
    {
        Debug.Log("running hurt");
        enemyBase.agent.SetDestination(enemyBase.ratNest.position);
        Debug.Log(enemyBase.agent.destination + " " + enemyBase.agent.nextPosition);
        Debug.Log(enemyBase.agent.remainingDistance);
        enemyBase.agent.speed = hurtSpeed;
        thisLookAt.enabled = false;
        isHurt = true;
        enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Hurt);
    }

    void FindNewDestination()
    {
        // find closest point to player around x unit circle, set as destination
        Vector3 dirToPlayer = Vector3.Normalize(enemyBase.player.transform.position - gameObject.transform.position);
        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, enemyBase.player.transform.position);
        float distToTravel = distanceFromPlayer - distanceDestinationWillBeFromPlayer;
        Vector3 pointCloseToPlayerDestination = gameObject.transform.position + (dirToPlayer * distToTravel);
        enemyBase.agent.SetDestination(pointCloseToPlayerDestination);

        destinationTimer = 0;
    }
}
