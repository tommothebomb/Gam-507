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
    float waitTime;


    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        deathPos = gameObject.transform.position;
        endPos = new Vector3(deathPos.x, deathPos.y - 6, deathPos.z);
        enemyBase.agent.SetDestination(deathPos);
        startDescent = false;
        lerpTime = 0; waitTime = 0;
        gameObject.GetComponent<Collider>().enabled = false;
        enemyBase.animator.SetTrigger("ToDeath");
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        if (!startDescent)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= 4) startDescent = true;
        }
        else
        {
            lerpTime += Time.deltaTime * 0.5f;
            gameObject.transform.position = Vector3.Lerp(deathPos, endPos, lerpTime);
            if (gameObject.transform.position.y <= -5) gameObject.SetActive(false);
        }
    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {
        base.DoAnimationTriggerLogic(trigger);
    }
}
