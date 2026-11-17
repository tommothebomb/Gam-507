using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "WoodlouseAttackState", menuName = "Scriptable Objects/Attack/WoodlouseAttackState")]
public class WoodlouseAttackState : EnemyAttackSOBase
{


    public override void DoEnterLogic()
    {
        Debug.Log("attacking");
    }

    public override void DoUpdateLogic()
    {

    }

    public override void DoAnimationTriggerLogic(EnemyBase.AnimationTriggers trigger)
    {

    }
}
