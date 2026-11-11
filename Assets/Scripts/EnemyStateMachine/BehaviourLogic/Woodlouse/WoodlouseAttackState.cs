using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "WoodlouseAttackState", menuName = "Scriptable Objects/Attack/WoodlouseAttackState")]
public class WoodlouseAttackState : EnemyAttackSOBase
{
    [SerializeField] float distToBackUp;
    bool attackPlaying;


    public override void DoEnterLogic()
    {
        attackPlaying = false;
        enemyBase.agent.SetDestination(enemyBase.player.transform.position);
    }

    public override void DoUpdateLogic()
    {
        // when spider gets close enough, trigger attack anim
        if (enemyBase.agent.remainingDistance <= 2 && !attackPlaying)
        {
            attackPlaying = true;
            enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack2);
        }
    }

    // back away from player before swapping back to chase
    void MoveBack()
    {
        Vector3 backAwayPos = gameObject.transform.position - (gameObject.transform.forward * -distToBackUp);
        enemyBase.agent.SetDestination(backAwayPos);
        enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
    }
    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        // deal damage at point in animation where spider pierces player
        if (trigger == EnemyBase.AnimationTriggers.Attack2)
        {
            enemyBase.player.GetComponent<IDamageable>().TakeDamage(enemyBase.damageDeals);
        }
        // at end of anim, set bools back, remove web, allow player to move again and back away
        else if (trigger == EnemyBase.AnimationTriggers.EndAttack2)
        {
            enemyBase.enemyAnimController.isAttacking = false;
            enemyBase.playerStuckWeb.SetActive(false);
            enemyBase.player.GetComponent<PlayerMovement>().enabled = true;
            MoveBack();
        }
    }
}
