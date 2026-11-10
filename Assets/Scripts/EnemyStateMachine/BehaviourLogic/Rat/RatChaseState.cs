using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "RatChaseState", menuName = "Scriptable Objects/Chase/RatChaseState")]
public class RatChaseState : EnemyChaseSOBase
{
    // depending on health will have two different behaviours
    // if health is low, will move slower and retreat from player to a designated 'nest'
    // else will chase player
    [SerializeField] float hurtSpeed;
    public bool isHurt;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        Debug.Log("entered chase");

    }

    public override void DoUpdateLogic() 
    {
        // check states and set back if needed
        if (!enemyBase.IsAggroable) enemyBase.stateMachine.ChangeState(enemyBase.patrolState);
        // if player within attack radius, swap to attack state
        if (enemyBase.IsWithinAttackDist) enemyBase.stateMachine.ChangeState(enemyBase.attackState);

        // swap to retreat if takes enough damage
        if (isHurt)
        {
            enemyBase.agent.speed = hurtSpeed;
            enemyBase.agent.SetDestination(enemyBase.ratNest.position);
        }
        else enemyBase.agent.SetDestination(enemyBase.player.transform.position);
    }

    public override void DoExitLogic() 
    { 

    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger) 
    { 

    }
}
