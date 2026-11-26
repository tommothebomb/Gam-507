using UnityEngine;

// Libby/Jordan Script \\
public class ScrapPickup : MonoBehaviour
{
    [SerializeField] ParticleSystem drawPS;
    [SerializeField] ParticleSystem collectPS;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<IScrapCollectable>().CollectScrap();
            drawPS.Stop();
            collectPS.Play();
            Destroy(gameObject);
        }
    }
}
