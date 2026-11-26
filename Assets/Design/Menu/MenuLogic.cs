using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    [SerializeField] Animator logo;
    [SerializeField] Animator startButtonAnimator;
    [SerializeField] Animator controlsButtonAnimator;
    [SerializeField] Animator creditsButtonAnimator;
    [SerializeField] Animator exitButtonAnimator;
    [SerializeField] Animator returnButtonAnimator;

    [SerializeField] Animator controlsHeaderAnimator;
    [SerializeField] Animator controlsTextAnimator;

    [SerializeField] Animator creditsHeaderAnimator;
    [SerializeField] Animator creditsTextAnimator;

    private bool isControlsOpen;
    private bool isCreditsOpen;

    void Start()
    {
        isControlsOpen = false;
        isCreditsOpen = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void UIElementsExit()
    {
        logo.SetTrigger("ExitScene");
        startButtonAnimator.SetTrigger("ExitScene");
        controlsButtonAnimator.SetTrigger("ExitScene");
        creditsButtonAnimator.SetTrigger("ExitScene");
        exitButtonAnimator.SetTrigger("ExitScene");

        returnButtonAnimator.SetTrigger("EnterScene");
    }

    IEnumerator ElementsEnter()
    {
        returnButtonAnimator.SetTrigger("ExitScene");

        yield return new WaitForSeconds(.75f);

        logo.SetTrigger("EnterScene");
        startButtonAnimator.SetTrigger("EnterScene");
        controlsButtonAnimator.SetTrigger("EnterScene");
        creditsButtonAnimator.SetTrigger("EnterScene");
        exitButtonAnimator.SetTrigger("EnterScene");
    }

    public void Controls()
    {
        isControlsOpen = true;
        UIElementsExit();

        controlsHeaderAnimator.SetTrigger("EnterScene");
        controlsTextAnimator.SetTrigger("EnterScene");
    }

    void CloseControls()
    {
        isControlsOpen = false;

        controlsHeaderAnimator.SetTrigger("ExitScene");
        controlsTextAnimator.SetTrigger("ExitScene");

        StartCoroutine(ElementsEnter());
    }

    public void Credits()
    {
        isCreditsOpen = true;
        UIElementsExit();

        creditsHeaderAnimator.SetTrigger("EnterScene");
        creditsTextAnimator.SetTrigger("EnterScene");
    }

    void CloseCredits()
    {
        isCreditsOpen = false;

        creditsHeaderAnimator.SetTrigger("ExitScene");
        creditsTextAnimator.SetTrigger("ExitScene");

        StartCoroutine(ElementsEnter());
    }

    public void Return()
    {
        if (isControlsOpen)
        {
            CloseControls();
        }
        else if (isCreditsOpen)
        {
            CloseCredits();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
