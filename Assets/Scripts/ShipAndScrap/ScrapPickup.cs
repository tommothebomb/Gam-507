using UnityEngine;

// Libby/Jordan Script \\
public class ScrapPickup : MonoBehaviour
{
    [SerializeField] ParticleSystem thisPS;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<IScrapCollectable>().CollectScrap();
            thisPS.Play();
            Destroy(gameObject);
        }
    }
}
