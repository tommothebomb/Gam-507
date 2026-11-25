using UnityEditor.Rendering;
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
        RaycastHit hit;
        foreach (Transform t in attackPoints)
        {
            Physics.SphereCast(t.position,attackRadius,t.transform.forward*3,out hit,3,attackMask);
            Debug.DrawRay(t.position, t.transform.forward*3, Color.blue,1);

            Debug.Log(hit.point);

            if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out IDamageable HitObj) == true)
            {
                HitObj.TakeDamage(Damage);
                return;
            }
        }
    }
}
