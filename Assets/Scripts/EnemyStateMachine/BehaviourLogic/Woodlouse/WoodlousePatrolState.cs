using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "WoodlousePatrolState", menuName = "Scriptable Objects/Patrol/WoodlousePatrolState")]
public class WoodlousePatrolState : EnemyPatrolSOBase
{
    // spider enemies will be relatively stationary in the patrol state, staying hidden from the player
    LookAtPlayer thisLookAt;
    bool atStartPos;
    Vector3 startPos;
    Vector3 startRot;
    float rotationTime;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        thisLookAt = gameObject.GetComponent<LookAtPlayer>();
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.eulerAngles;
        atStartPos = true;
    }

    public override void DoEnterLogic()
    {
        // turn off shoot point and look at to save on processing
        thisLookAt.enabled = false;
        enemyBase.shootPoint.gameObject.SetActive(false);
        enemyBase.agent.SetDestination(startPos);
        rotationTime = 0;
    }

    public override void DoUpdateLogic()
    {
        // change state when player within aggro zone and aggro radius
        if (enemyBase.IsAggroable && enemyBase.IsAggroed) enemyBase.stateMachine.ChangeState(enemyBase.chaseState);

        // while returning to start position, once close enough make enemy turn to face the correct direction
        if (atStartPos) return;
        if (Vector3.Distance(gameObject.transform.position, startPos) <= 4)
        {
            rotationTime += Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(startRot), rotationTime * 0.1f);
            if (rotationTime >= 1.5f) atStartPos = true;
        }
    }

    public override void DoExitLogic()
    {
        thisLookAt.enabled = true;
        atStartPos = false;
    }
}
