using UnityEngine;

// Libby/Jordan Script \\
public class EnemyPatrolSOBase : ScriptableObject
{
    protected EnemyBase enemyBase;
    protected Transform transform;
    protected GameObject gameObject;
    GameObject healthBar;


    public virtual void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemyBase = enemyBase;
    }

    public virtual void DoEnterLogic() 
    {
        if (healthBar != null)
        {
            healthBar.SetActive(false);
        }
    }
    public virtual void DoUpdateLogic() { }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoExitLogic() 
    {
        healthBar = enemyBase.pooler.SpawnFromPool("healthBar", gameObject.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(gameObject.transform);
        healthBar.GetComponentInChildren<EnemyHealthBarManager>().enemyBase = enemyBase;
    }
    public virtual void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger) { }
}
