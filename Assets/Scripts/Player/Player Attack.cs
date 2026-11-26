using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform[] attackPoints;

    [SerializeField] LayerMask attackMask;

    [SerializeField] float attackRadius;

    [SerializeField] bool hasHit;

    [SerializeField] float Damage;

   public void AttackKeyFrame()
    {
        AkUnitySoundEngine.PostEvent("Play_AttackSwing", this.gameObject);
        RaycastHit hit;
        foreach (Transform t in attackPoints)
        {
            Physics.SphereCast(t.position,attackRadius,t.transform.forward * 2,out hit,attackMask);
            Debug.DrawRay(t.position, t.transform.forward * 2, Color.blue,1);

            //Debug.Log(hit.point);

            if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out IDamageable HitObj) == true)
            {
                HitObj.TakeDamage(Damage);
                return;
            }
            else if (hit.collider != null && hit.collider.TryGetComponent<Tap>(out Tap TapObj) == true)
            {
                TapObj.ManageTap();
            }
        }
    }
    public void AttackCharge()
    {
        AkUnitySoundEngine.PostEvent("Play_AttackCharge", this.gameObject);
    }
}
