using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    

    public void StartGameButton()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("KitchenLevelGreybox");
        
        
    }
    
}