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
    float timeBetweenDestinationSet = 0.5f;
    float distanceDestinationWillBeFromPlayer = 5f;


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
        if (enemyBase.currentHealth <= healtAmountToRetreat) SetHurtLogic();
    }

    public override void DoUpdateLogic()
    {
        if (enemyBase.currentHealth <= healtAmountToRetreat && !isHurt) // to prevent running every frame when doesn't need to
        {
            SetHurtLogic();
        }
        else if (!isHurt) // normal state 
        {
            destinationTimer += Time.deltaTime;
            if (destinationTimer >= timeBetweenDestinationSet) FindNewDestination();
        }

        if (isHurt) // nesting for performance, dont want to call vector3.distance more than i need to
        {
            //if (enemyBase.agent.destination != enemyBase.ratNest.position) SetHurtLogic();
            if (Vector3.Distance(gameObject.transform.position, enemyBase.ratNest.position) <= 4)
            {
                gameObject.SetActive(false);
                isHurt = false;
            }
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
        enemyBase.agent.SetDestination(enemyBase.ratNest.position);
        enemyBase.agent.speed = hurtSpeed;
        thisLookAt.enabled = false;
        enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Hurt);
        isHurt = true;
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
