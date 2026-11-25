using UnityEngine;

// Libby/Jordan Script \\
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    // could be used to make a save system later
    [Header("Ship Repair")]
    public int currentScrap = 0;

    [Header("Player Health")]
    public float maxHealth;
    public float currentHealth;
}
