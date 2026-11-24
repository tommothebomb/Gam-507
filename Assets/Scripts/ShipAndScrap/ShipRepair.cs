using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Libby/Jordan Script \\
public class ShipRepair : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] ShipHealth shipHealth;
    [SerializeField] Transform player;
    [SerializeField] Slider shipHealthSlider;
    [SerializeField] Slider shipRepairSlider;
    InputSystem_Actions inputActs;
    bool isHoldingRepair;


    #region InputSystem Stuff
    private void OnEnable()
    {
        inputActs = new InputSystem_Actions();
        inputActs.Player.Enable();
        inputActs.Player.Interact.started += ctx => StartRepair();
        inputActs.Player.Interact.canceled += ctx => EndRepair();
    }
    private void OnDisable() => inputActs.Player.Disable();
    #endregion
    private void Start()
    {
        isHoldingRepair = false;
    }

    private void Update()
    {
        // if player holds E with at least one scrap will repair ship
        if (isHoldingRepair && stats.currentScrap > 0)
        {
            shipRepairSlider.value += Time.deltaTime;
            if (shipRepairSlider.value >= 1)
            {
                shipRepairSlider.value = 0;
                UpdateShipHealthUI();
            }
        }

        LookAtPlayer();
    }

    void StartRepair() => isHoldingRepair = true;
    void EndRepair() => isHoldingRepair = false;

    void UpdateShipHealthUI()
    {
        shipHealth.currentHealth += stats.currentScrap;
        stats.currentScrap = 0;
        float currentRepair = shipHealth.currentHealth / shipHealth.targetHealth;
        Debug.Log(shipHealth.currentHealth + " " + shipHealth.targetHealth);
        Debug.Log(currentRepair);
        shipHealthSlider.value = currentRepair;

        if (shipHealth.currentHealth >= shipHealth.targetHealth)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    void LookAtPlayer()
    {
        Vector3 playerFlat = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(playerFlat);
    }
}
