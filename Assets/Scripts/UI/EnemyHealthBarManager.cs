using UnityEngine;
using UnityEngine.UI;

// Libby/Jordan Script \\
public class EnemyHealthBarManager : MonoBehaviour
{
    public EnemyBase enemyBase;
    [SerializeField] Slider healthBar;


    private void Update()
    {
        healthBar.value = enemyBase.currentHealth / enemyBase.MaxHealth;
    }
}
