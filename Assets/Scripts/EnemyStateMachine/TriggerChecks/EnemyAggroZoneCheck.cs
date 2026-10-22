using UnityEngine;

// Libby/Jordan Script \\
public class EnemyAggroZoneCheck : MonoBehaviour
{
    // place this on the aggro zone trigger

    [Tooltip ("Every enemy in this trigger zone must be in this array to be able to aggro")]
    [SerializeField] EnemyBase[] enemiesInZone;


    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < enemiesInZone.Length; i++)
            {
                EnemyBase thisBase = enemiesInZone[i].GetComponentInChildren<EnemyBase>();
                thisBase.SetAggroZoneStatus(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < enemiesInZone.Length; i++)
            {
                EnemyBase thisBase = enemiesInZone[i].GetComponentInChildren<EnemyBase>();
                thisBase.SetAggroZoneStatus(false);
            }
        }
    }
}
