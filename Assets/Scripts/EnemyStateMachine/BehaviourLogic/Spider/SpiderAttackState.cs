using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderAttackState", menuName = "Scriptable Objects/Attack/SpiderAttackState")]
public class SpiderAttackState : EnemyAttackSOBase
{
    [SerializeField] float distToBackUp;
    bool attackPlaying;
    bool movingBack;


    public override void DoEnterLogic()
    {
        attackPlaying = false;
        movingBack = false;
        enemyBase.agent.SetDestination(enemyBase.player.transform.position);
        enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Moving);
    }

    public override void DoUpdateLogic()
    {
        // when spider gets close enough, trigger attack anim
        if (enemyBase.agent.remainingDistance <= 2 && !attackPlaying)
        {
            attackPlaying = true;
            enemyBase.agent.isStopped = true;
            enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack2);
        }

        // when backed away swap back to chase
        if (movingBack && enemyBase.agent.remainingDistance < 0.2f)
        {
            enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
            Debug.Log("changed state");
        }
    }

    // back away from player
    void MoveBack()
    {
        enemyBase.agent.isStopped = false;
        Vector3 backAwayPos = gameObject.transform.position + (gameObject.transform.forward * -distToBackUp);
        enemyBase.agent.SetDestination(backAwayPos);
        movingBack = true;
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
            MoveBack();

            enemyBase.playerStuckWeb.SetActive(false);
            enemyBase.player.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
