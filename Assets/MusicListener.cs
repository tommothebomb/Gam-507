using UnityEngine;

public class MusicListener : MonoBehaviour
{
    
    public AK.Wwise.Event mainMenuMusicEvent;

    void Awake()
    {
        mainMenuMusicEvent.Post(gameObject);
    }
}