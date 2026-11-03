using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderAttackState", menuName = "Scriptable Objects/Attack/SpiderAttackState")]
public class SpiderAttackState : EnemyAttackSOBase
{
    [SerializeField] float distToBackUp;
    bool attackPlaying;
    enum animationStates { idle, walking, attacking }


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
            // play attack animation
            // deal damage to player
            // destroy web 
            MoveBack();
        }
    }

    void MoveBack()
    {
        Vector3 backAwayPos = gameObject.transform.position - (gameObject.transform.forward * -distToBackUp);
        enemyBase.agent.SetDestination(backAwayPos);
        enemyBase.animator.SetTrigger("ToTakeDamage");
        enemyBase.stateMachine.ChangeState(enemyBase.deathState);
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
        // damage player and destroy web
    }
}
