using UnityEngine;
using UnityEngine.AI;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "WoodlousePatrolState", menuName = "Scriptable Objects/Patrol/WoodlousePatrolState")]
public class WoodlousePatrolState : EnemyPatrolSOBase
{
    // spider enemies will be relatively stationary in the patrol state, staying hidden from the player
    LookAtPlayer thisLookAt;
    Vector3 startPos;
    [SerializeField] float maxRoamRadius;
    float minRoamRadius;
    Vector3 randomPoint;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
        thisLookAt = gameObject.GetComponent<LookAtPlayer>();
        startPos = gameObject.transform.position;
        minRoamRadius = maxRoamRadius * -1;
    }

    public override void DoEnterLogic()
    {
        // turn off shoot point and look at to save on processing
        thisLookAt.enabled = false;
        FindRandomPointOnNavmesh();
    }

    public override void DoUpdateLogic()
    {
        // change state when player within aggro zone and aggro radius
        if (enemyBase.IsAggroable && enemyBase.IsAggroed) enemyBase.stateMachine.ChangeState(enemyBase.chaseState);

        // if agent reaches destination, find a new one
        if (enemyBase.agent.remainingDistance < 0.5f) FindRandomPointOnNavmesh();
    }

    void FindRandomPointOnNavmesh()
    {
        float randomX = startPos.x + Random.Range(minRoamRadius, maxRoamRadius);
        float randomZ = startPos.z + Random.Range(minRoamRadius, maxRoamRadius);
        randomPoint = new Vector3(randomX, startPos.y, randomZ);

        if (CheckPointValid()) enemyBase.agent.SetDestination(randomPoint);
        else FindRandomPointOnNavmesh();
    }

    bool CheckPointValid()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2, NavMesh.AllAreas)) return true;
        else return false;
    }

    public override void DoExitLogic()
    {
        thisLookAt.enabled = true;
    }
}
