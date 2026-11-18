using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "RatChaseState", menuName = "Scriptable Objects/Chase/RatChaseState")]
public class RatChaseState : EnemyChaseSOBase
{
    // depending on health will have two different behaviours
    // if health is low, will move slower and retreat from player to a designated 'nest'
    // else will chase player
    LookAtPlayer thisLookAt;
    [SerializeField] float healtAmountToRetreat;
    [SerializeField] float hurtSpeed;
    bool isHurt;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        thisLookAt = gameObject.GetComponent<LookAtPlayer>();
        isHurt = false;
    }

    public override void DoEnterLogic()
    {

    }

    public override void DoUpdateLogic()
    {
        if (enemyBase.CurrentHealth <= healtAmountToRetreat && !isHurt) // to prevent running every frame when doesn't need to
        {
            isHurt = true;
            thisLookAt.enabled = false;
            enemyBase.agent.speed = hurtSpeed;
            enemyBase.agent.SetDestination(enemyBase.ratNest.position);
        }
        else if (!isHurt) // normal state
        { 
            enemyBase.agent.SetDestination(enemyBase.player.transform.position); 
        }

        // check aggro and set back to patrol if needed
        if (!enemyBase.IsAggroable) enemyBase.stateMachine.ChangeState(enemyBase.patrolState);
        // if player within attack radius, swap to attack state
        if (enemyBase.IsWithinAttackDist && !isHurt) enemyBase.stateMachine.ChangeState(enemyBase.attackState);
    }

    public override void DoExitLogic()
    {

    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {

    }
}
