using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene("KitchenLevelGreybox");
    }
}
