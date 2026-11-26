using UnityEngine;

public class PlayerScrapCollect : MonoBehaviour, IScrapCollectable
{
    [SerializeField] PlayerStats stats;
    public AK.Wwise.Event ItemGetEvent;

    // add one scrap every time this is called
    public void CollectScrap()
    {
        stats.currentScrap++;
        Debug.Log(stats.currentScrap);
        ItemGetEvent.Post(gameObject);
    }
}
