using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderAttackState", menuName = "Scriptable Objects/Attack/SpiderAttackState")]
public class SpiderAttackState : EnemyAttackSOBase
{
    /*[Tooltip("make sure this is smaller than the aggro check trigger")]
    [SerializeField] float rangedAttackDist;
    [Tooltip("make sure this is smaller than the attack range check trigger")]
    [SerializeField] float meleeAttackDist;*/
    bool attackPlaying;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("in attack state");
        attackPlaying = false;
        enemyBase.agent.SetDestination(enemyBase.player.transform.position);
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        if (enemyBase.IsWithinAttackDist && !attackPlaying)
        {
            attackPlaying = true;
            Debug.Log("attack");
            // play attack animation
            // deal damage to player
            // destroy web 
            MoveBack();
        }
    }

    void MoveBack()
    {
        // move back 
        Vector3 backAwayPos = gameObject.transform.position - (gameObject.transform.forward * -5);
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
    }
}
