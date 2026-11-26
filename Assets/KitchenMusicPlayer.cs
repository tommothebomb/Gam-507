using UnityEngine;

public class KitchenMusicPlayer : MonoBehaviour
{
    public AK.Wwise.Event KitchenMusicEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KitchenMusicEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
