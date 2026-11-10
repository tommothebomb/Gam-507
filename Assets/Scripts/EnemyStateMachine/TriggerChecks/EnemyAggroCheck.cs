using UnityEngine;

// Libby/Jordan Script \\
public class EnemyAggroCheck : MonoBehaviour
{
    // place this script on the enemy aggro trigger
    // trigger MUST be child of enemy

    EnemyBase thisBase;


    private void Awake()
    {
        thisBase = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // only set aggroed to true if player in same zone as enemy
        if (other.CompareTag("Player") && thisBase.IsAggroable)
        {
            thisBase.SetAggroStatus(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thisBase.SetAggroStatus(false);
        }
    }
}
