using UnityEngine;

// Libby/Jordan Script \\
public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }


    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Debug.Log("playerDied");
        }
    }
}
