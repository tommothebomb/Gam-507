using UnityEngine;

// Libby/Jordan Script \\
public interface ITriggerCheckable
{
    bool IsAggroable { get; set; }
    bool IsAggroed { get; set; }
    bool IsWithinAttackDist { get; set; }
    public void SetAggroZoneStatus(bool isAgroable) { }
    public void SetAggroStatus(bool isAgroed) { }
    public void SetAttackDistBool(bool isWithinAttackDist) { }
}
