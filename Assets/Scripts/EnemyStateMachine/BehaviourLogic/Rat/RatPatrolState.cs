using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "RatPatrolState", menuName = "Scriptable Objects/Patrol/RatPatrolState")]
public class RatPatrolState : EnemyPatrolSOBase
{
    // want this to have the rats sitting around an area eating food
    // eating will be an animation loop
    // will go back to starting points when swap back to this state, need to set starting pos when initialize
    bool atStartPos;
    Vector3 startPos;
    Vector3 startRot;
    float rotationTime;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.eulerAngles;
        atStartPos = true;
    }

    public override void DoEnterLogic()
    {
        Debug.Log("entered patrol");
        enemyBase.agent.SetDestination(startPos);
        rotationTime = 0;
    }
    public override void DoUpdateLogic()
    {
        // will chase player as soon as they enter rats zone, dont need to be within certain radius
        if (enemyBase.IsAggroable) enemyBase.stateMachine.ChangeState(enemyBase.chaseState);

        // while returning to start position, once close enough make enemy turn to face the correct direction
        if (atStartPos) return;
        if (Vector3.Distance(gameObject.transform.position, startPos) <= 2)
        {
            rotationTime += Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(startRot), rotationTime * 0.1f);
            if (rotationTime >= 1.5f) atStartPos = true;
        }
    }
    public override void DoPhysicsUpdateLogic()
    {

    }
    public override void DoExitLogic()
    {

    }
    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {

    }
}
