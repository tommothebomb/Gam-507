using UnityEngine;

public class TestDamageable : MonoBehaviour, IDamageable
{
    public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log("owwi.. that hurt");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
}
