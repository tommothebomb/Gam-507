using UnityEngine;

// Libby/Jordan Script \\
public class EnemyActivationZoneCheck : MonoBehaviour
{
    // place this on the activation zone trigger

    [Tooltip ("Every enemy in this trigger zone must be in this array to be able to activate")]
    [SerializeField] GameObject[] enemiesInZone;
    [SerializeField] bool activeOnStart;


    private void Start()
    {
        // if the enemies in this zone shouldn't be active on game start, itterate through array and deactivate all
        if (!activeOnStart)
        {
            for (int i = 0; i < enemiesInZone.Length; i++)
            {
                enemiesInZone[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < enemiesInZone.Length; i++)
            {
                enemiesInZone[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < enemiesInZone.Length; i++)
            {
                enemiesInZone[i].SetActive(false);
            }
        }
    }
}
