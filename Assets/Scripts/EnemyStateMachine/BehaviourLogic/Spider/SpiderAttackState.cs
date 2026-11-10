using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderAttackState", menuName = "Scriptable Objects/Attack/SpiderAttackState")]
public class SpiderAttackState : EnemyAttackSOBase
{
    [SerializeField] float distToBackUp;
    bool attackPlaying;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        attackPlaying = false;
        enemyBase.agent.SetDestination(enemyBase.player.transform.position);
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        if (enemyBase.IsWithinAttackDist && !attackPlaying)
        {
            attackPlaying = true;
            enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Attack2);
        }
    }

    void MoveBack()
    {
        Vector3 backAwayPos = gameObject.transform.position - (gameObject.transform.forward * -distToBackUp);
        enemyBase.agent.SetDestination(backAwayPos);
        enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
    }

    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        base.DoAnimationTriggerLogic(trigger);

        if (trigger == EnemyBase.AnimationTriggers.Attack2)
        {
            enemyBase.player.GetComponent<IDamageable>().TakeDamage(enemyBase.damageDeals);
        }
        else if (trigger == EnemyBase.AnimationTriggers.EndAttack2)
        {
            enemyBase.playerStuckWeb.SetActive(false);
            enemyBase.player.GetComponent<PlayerMovement>().enabled = true;
            enemyBase.enemyAnimController.isAttacking = false;
            MoveBack();
        }
    }
}
