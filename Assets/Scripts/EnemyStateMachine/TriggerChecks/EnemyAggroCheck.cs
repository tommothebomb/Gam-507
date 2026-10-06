using UnityEngine;

// Libby/Jordan Script \\
public class EnemyAggroCheck : MonoBehaviour
{
    // place this script on the enemy aggro trigger
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
            thisBase.SetAggroStatus(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            thisBase.SetAggroStatus(false);
        }
    }
}
