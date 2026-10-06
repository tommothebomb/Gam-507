using UnityEngine;

// Libby/Jordan Script \\
public class EnemyAggroZoneCheck : MonoBehaviour
{
    // place this on the aggro zone trigger

    [Tooltip ("Every enemy in this trigger zone must be in this array to be able to aggro")]
    [SerializeField] EnemyBase[] enemiesInZone;
    GameObject player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            for (int i = 0; i < enemiesInZone.Length; i++)
            {
                EnemyBase thisBase = enemiesInZone[i].GetComponentInParent<EnemyBase>();
                thisBase.SetAggroZoneStatus(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            for (int i = 0; i < enemiesInZone.Length; i++)
            {
                EnemyBase thisBase = enemiesInZone[i].GetComponentInParent<EnemyBase>();
                thisBase.SetAggroZoneStatus(false);
            }
        }
    }
}
