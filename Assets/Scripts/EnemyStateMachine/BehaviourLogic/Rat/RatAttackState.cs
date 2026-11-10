using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "RatAttackState", menuName = "Scriptable Objects/Attack/RatAttackState")]
public class RatAttackState : EnemyAttackSOBase
{
    // when player is within attack trigger will swap to this state
    // will do a lunge attack before retreating back a small amount, similar to spider
    // this could be entirely animation/anitmation trigger based if the designers model and animate the rat
    bool hasAttacked;
    float i;


    public override void DoEnterLogic()
    {
        Debug.Log("entered attack");
        hasAttacked = false;
        enemyBase.agent.isStopped = true;
        enemyBase.agent.ResetPath();
        enemyBase.agent.SetDestination(gameObject.transform.position);
        enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack1);
        Debug.Log("Rat position: " + gameObject.transform.position);
        Debug.Log("Agent destination: " + enemyBase.agent.destination);
        Debug.LogError("aaaaaaaaaaaaaaaaa");
    }

    public override void DoUpdateLogic()
    {
        if (enemyBase.agent.remainingDistance <= 1 && hasAttacked) enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
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
            Debug.Log("attacked");
        }
        else if (trigger == EnemyBase.AnimationTriggers.EndAttack1) { enemyBase.enemyAnimController.isAttacking = false; hasAttacked = true; }
    }
}
