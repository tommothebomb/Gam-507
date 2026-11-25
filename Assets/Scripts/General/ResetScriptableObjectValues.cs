using UnityEngine;

// Libby/Jordan Script \\
public class ResetScriptableObjectValues : MonoBehaviour
{
    // run this script when starting a new game
    // DO NOT place this within either of the game levels
    [Header("Player Stats")]
    [SerializeField] PlayerStats playerStats;
    [SerializeField] float maxHealth;

    [Header("Ship Health")]
    [SerializeField] ShipHealth shipHealth;
    [Tooltip("set this to the number of scrap that can be found between all levels")]
    [SerializeField] float targetHealth;


    private void Start()
    {
        playerStats.currentScrap = 0;
        playerStats.maxHealth = maxHealth;
        playerStats.currentHealth = maxHealth;

        shipHealth.targetHealth = targetHealth;
        shipHealth.currentHealth = 0;
    }
}
