using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "RatAttackState", menuName = "Scriptable Objects/Attack/RatAttackState")]
public class RatAttackState : EnemyAttackSOBase
{
    // when player is within attack trigger will swap to this state
    // will do a lunge attack before retreating back a small amount, similar to spider
    // this could be entirely animation/anitmation trigger based if the designers model and animate the rat
    Vector3 moveBackPos;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        // will set destinastion to current transform.pos and play attack anim
        Debug.Log("attack player");
        enemyBase.agent.SetDestination(gameObject.transform.position);
        moveBackPos = gameObject.transform.position - (gameObject.transform.forward * 6);
        Debug.Log(moveBackPos);
    }
    public override void DoUpdateLogic()
    {
        if (enemyBase.agent.destination == moveBackPos && enemyBase.agent.remainingDistance <= 1) enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
    }

    public override void DoPhysicsUpdateLogic()
    {

    }

    public override void DoExitLogic()
    {

    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        if (trigger == EnemyBase.AnimationTriggers.Attack1)
        {
            enemyBase.player.GetComponent<IDamageable>().TakeDamage(enemyBase.damageDeals);
        }
        else if (trigger == EnemyBase.AnimationTriggers.EndAttack1) enemyBase.enemyAnimController.isAttacking = false;
    }
}
