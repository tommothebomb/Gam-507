using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "WoodlouseAttackState", menuName = "Scriptable Objects/Attack/WoodlouseAttackState")]
public class WoodlouseAttackState : EnemyAttackSOBase
{
    BoxCollider thisCol;
    Vector3 sizeWhenWalking = new Vector3(3, 1.5f, 5.4f);
    [SerializeField] float maxRollDistance;
    bool isRolling;
    [SerializeField] float rollSpeed;
    [SerializeField] float rollAcceleration;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        thisCol = gameObject.GetComponent<BoxCollider>();
    }

    public override void DoEnterLogic()
    {
        isRolling = true;
        enemyBase.hitCollider = false;
        enemyBase.agent.speed = rollSpeed;
        enemyBase.agent.acceleration = rollAcceleration;
        enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack2);
        FindDestinationInPlayerDirection();
    }

    public override void DoUpdateLogic()
    {
        // if hits something or runs out of time, swap to stuck on back anim, 
        if (enemyBase.agent.remainingDistance < 0.2f && isRolling || enemyBase.hitCollider && isRolling)
        {
            isRolling = false;
            enemyBase.hitCollider = false;
            thisCol.size = sizeWhenWalking;
            enemyBase.agent.isStopped = true;
            enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack3);
            enemyBase.takeLessDamage = false;
            if (enemyBase.hitPlayerCollider) enemyBase.player.GetComponent<IDamageable>().TakeDamage(enemyBase.damageDeals);
            enemyBase.hitPlayerCollider = false;
        }
    }

    void FindDestinationInPlayerDirection()
    {
        Vector3 playerDir = Vector3.Normalize(enemyBase.player.transform.position - gameObject.transform.position);
        Vector3 newDestination = gameObject.transform.position + (playerDir * maxRollDistance);
        enemyBase.agent.SetDestination(newDestination);
    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        if (trigger == EnemyBase.AnimationTriggers.EndAttack3)
        {
            enemyBase.enemyAnimController.isAttacking = false;
            enemyBase.agent.isStopped = false;
            enemyBase.agent.speed = enemyBase.speed;
            enemyBase.agent.acceleration = enemyBase.acceleration;
            enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
        }
    }
}
