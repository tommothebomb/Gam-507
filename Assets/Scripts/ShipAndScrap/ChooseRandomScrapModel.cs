using UnityEngine;

// Libby/Jordan Script \\
public class ChooseRandomScrapModel : MonoBehaviour
{
    [SerializeField] GameObject[] scrapModels;


    private void Start()
    {
        int randomNum = Random.Range(0, scrapModels.Length);
        GameObject thisScrap = Instantiate(scrapModels[randomNum], transform.position, Random.rotation);
        thisScrap.transform.SetParent(gameObject.transform);
    }
}
