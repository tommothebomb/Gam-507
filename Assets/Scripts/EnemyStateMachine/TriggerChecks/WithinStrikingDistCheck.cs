using UnityEngine;

// Libby/Jordan Script \\
public class WithinStrikingDistCheck : MonoBehaviour
{
    // place this script on the enemy attack distance trigger
    // trigger MUST be child of enemy

    GameObject player;
    EnemyBase thisBase;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        thisBase = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            thisBase.SetAttackDistBool(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            thisBase.SetAttackDistBool(false);
        }
    }
}
