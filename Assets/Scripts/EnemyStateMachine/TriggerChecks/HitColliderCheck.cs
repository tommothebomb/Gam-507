using UnityEngine;

// Libby/Jordan Script \\
public class HitColliderCheck : MonoBehaviour
{
    // place this script on woodlouse enemy ONLY

    EnemyBase thisBase;


    private void Awake()
    {
        thisBase = GetComponent<EnemyBase>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        thisBase.hitCollider = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            thisBase.hitPlayerCollider = true;
        }
    }
}
