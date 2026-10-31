using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "SpiderPatrolState", menuName = "Scriptable Objects/Patrol/SpiderPatrolState")]
public class SpiderPatrolState : EnemyPatrolSOBase
{
    // spider enemies will be relatively stationary in the patrol state, staying hidden from the player
    ShootPointLookAt thisLookAt;
    bool atStartPos;
    Vector3 startPos;
    Vector3 startRot;
    float rotationTime;

    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        thisLookAt = gameObject.GetComponent<ShootPointLookAt>();
        thisLookAt.enabled = false;
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.eulerAngles;
        atStartPos = true;
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemyBase.shootPoint.gameObject.SetActive(false);
        enemyBase.agent.SetDestination(startPos);
        rotationTime = 0;
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        if (enemyBase.IsAggroable && enemyBase.IsAggroed) enemyBase.stateMachine.ChangeState(enemyBase.chaseState);

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
        base.DoPhysicsUpdateLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        thisLookAt.enabled = true;
        atStartPos = false;
    }
    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        base.DoAnimationTriggerLogic(trigger);
        // play idle animation
    }
}
