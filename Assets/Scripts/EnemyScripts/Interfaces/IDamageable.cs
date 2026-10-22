
// Libby/Jordan Script \\
public interface IDamageable
{
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
    public void TakeDamage(float damageAmount);
}
