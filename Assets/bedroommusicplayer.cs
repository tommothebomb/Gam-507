using UnityEngine;

public class bedroommusicplayer : MonoBehaviour
{
    public AK.Wwise.Event bedroomMusicEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bedroomMusicEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
