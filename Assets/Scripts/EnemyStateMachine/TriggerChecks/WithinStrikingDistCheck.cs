using UnityEngine;

// Libby/Jordan Script \\
public class WithinStrikingDistCheck : MonoBehaviour
{
    // place this script on the enemy attack distance trigger
    // trigger MUST be child of enemy

    EnemyBase thisBase;


    private void Awake()
    {
        thisBase = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thisBase.SetAttackDistBool(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thisBase.SetAttackDistBool(false);
        }
    }
}
