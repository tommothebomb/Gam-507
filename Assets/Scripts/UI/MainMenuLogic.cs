using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public bool Starting;

    public void StartGameButton()
    {
        if (Starting) return;
        Starting = true;
        StartCoroutine(startgameroutine());
    }

    IEnumerator startgameroutine()
    {
        yield return new WaitForSeconds(2f);
        AkUnitySoundEngine.StopAll();
        SceneManager.LoadScene("KitchenLevelGreybox");
    }
}