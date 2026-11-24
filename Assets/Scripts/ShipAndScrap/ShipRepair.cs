using UnityEngine;
using UnityEngine.UI;

// Libby/Jordan Script \\
public class ShipRepair : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] ShipHealth shipHealth;
    [SerializeField] Transform player;
    [SerializeField] Slider shipHealthSlider;
    [SerializeField] Slider shipRepairSlider;
    


    private void Update()
    {


        LookAtPlayer();
    }

    void UpdateShipHealthUI()
    {
        float currentRepair = shipHealth.targetHealth / shipHealth.currentHealth;
        shipHealthSlider.value = currentRepair;
    }

    void LookAtPlayer()
    {
        Vector3 playerFlat = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(playerFlat);
    }
}
