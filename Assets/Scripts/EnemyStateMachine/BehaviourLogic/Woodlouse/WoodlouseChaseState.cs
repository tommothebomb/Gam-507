using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "WoodlouseChaseState", menuName = "Scriptable Objects/Chase/WoodlouseChaseState")]
public class WoodlouseChaseState : EnemyChaseSOBase
{
    LookAtPlayer thisLookAt;
    [SerializeField] float chargeTimerMax;
    [SerializeField] float chargeTimerMin;
    float chargeTimer;
    [SerializeField] float distanceDestinationWillBeFromPlayer;
    float timeCharging;
    bool isCharging;
    float moveInterval = 0.5f;
    float moveTimer;
    BoxCollider thisCol;
    Vector3 sizeWhenRolling = new Vector3(3, 1.5f, 3);


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        thisCol = gameObject.GetComponent<BoxCollider>();
        thisLookAt = gameObject.GetComponent<LookAtPlayer>();
    }

    public override void DoEnterLogic()
    {
        thisLookAt.enabled = true;
        moveTimer = 0;
        timeCharging = 0;
        isCharging = false;
        chargeTimer = Random.Range(chargeTimerMin, chargeTimerMax);
        FindPointAroundPlayer();
    }

    public override void DoUpdateLogic()
    {        
        if (!isCharging)
        {
            // follow player while keeping distance, having very small timer to save on processing power where possible without impacting game feel
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveInterval) { moveTimer = 0; FindPointAroundPlayer(); }

            // count down to charge
            timeCharging += Time.deltaTime;
            if (timeCharging >= chargeTimer)
            {
                isCharging = true;
                enemyBase.agent.SetDestination(gameObject.transform.position);
                enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack1); // Attack1 will be to curl the woodlouse into a ball
                enemyBase.takeLessDamage = true;
            }
        }

        // check states and set back if needed
        if (!enemyBase.IsAggroable || !enemyBase.IsAggroed) enemyBase.stateMachine.ChangeState(enemyBase.patrolState);
    }

    void FindPointAroundPlayer()
    {
        // find closest point to player around x unit circle, set as destination
        Vector3 dirToPlayer = Vector3.Normalize(enemyBase.player.transform.position - gameObject.transform.position);
        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, enemyBase.player.transform.position);
        float distToTravel = distanceFromPlayer - distanceDestinationWillBeFromPlayer;
        Vector3 pointCloseToPlayerDestination = gameObject.transform.position + (dirToPlayer * distToTravel);
        enemyBase.agent.SetDestination(pointCloseToPlayerDestination);
    }

    public override void DoExitLogic()
    {
        thisLookAt.enabled = false;
    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        if (trigger == EnemyBase.AnimationTriggers.Attack1) { thisCol.size = sizeWhenRolling; }
        else if (trigger == EnemyBase.AnimationTriggers.EndAttack1) { enemyBase.stateMachine.ChangeState(enemyBase.attackState); } // not setting animator attacking bool to false as will immediately enter next attack animation in attack state
    }
}
