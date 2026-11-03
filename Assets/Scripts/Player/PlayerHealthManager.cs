using UnityEngine;

// Libby/Jordan Script \\
public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }


    public void TakeDamage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }
}
