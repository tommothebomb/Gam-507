using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "RatAttackState", menuName = "Scriptable Objects/Attack/RatAttackState")]
public class RatAttackState : EnemyAttackSOBase
{
    // when player is within attack trigger will swap to this state
    // will do a lunge attack before retreating back a small amount, similar to spider
    // this could be entirely animation/anitmation trigger based if the designers model and animate the rat


    public override void DoEnterLogic()
    {
        enemyBase.agent.isStopped = true;
        enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack1);
    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        if (trigger == EnemyBase.AnimationTriggers.Attack1)
        {
            if (enemyBase.IsWithinAttackDist) enemyBase.player.GetComponent<IDamageable>().TakeDamage(enemyBase.damageDeals);
        }
        else if (trigger == EnemyBase.AnimationTriggers.EndAttack1)
        {
            enemyBase.enemyAnimController.isAttacking = false;
            enemyBase.agent.isStopped = false;
            enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
        }
    }
}
