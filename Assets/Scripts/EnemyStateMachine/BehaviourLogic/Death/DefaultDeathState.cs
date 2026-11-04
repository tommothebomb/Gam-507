using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "DefaultDeathState", menuName = "Scriptable Objects/Death/DefaultDeathState")]
public class DefaultDeathState : EnemyDeathSOBase
{
    // delete/deactivate model (depending on wether pooled or not)
    bool startDescent;
    Vector3 deathPos;
    Vector3 endPos;
    float lerpTime;


    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        deathPos = gameObject.transform.position;
        endPos = new Vector3(deathPos.x, deathPos.y - 6, deathPos.z);
        enemyBase.agent.SetDestination(deathPos);
        startDescent = false;
        gameObject.GetComponent<Collider>().enabled = false;
        enemyBase.enemyAnimController.ChangeAnimation(EnemyAnimations.AnimationStates.Death);
        lerpTime = 0; 
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();

        // make enemy sink below the floor before deactivating
        if (startDescent)
        {
            lerpTime += Time.deltaTime * 0.5f;
            gameObject.transform.position = Vector3.Lerp(deathPos, endPos, lerpTime);
            if (gameObject.transform.position.y <= -5) gameObject.SetActive(false);
        }
    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        base.DoAnimationTriggerLogic(trigger);
        startDescent = true;
    }
}
