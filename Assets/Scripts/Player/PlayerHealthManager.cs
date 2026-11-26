using UnityEngine;
using UnityEngine.SceneManagement;

// Libby/Jordan Script \\
public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStats stats;


    public void TakeDamage(float damageAmount)
    {
        stats.currentHealth -= damageAmount;
        Debug.Log(stats.currentHealth);
        if (stats.currentHealth <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
