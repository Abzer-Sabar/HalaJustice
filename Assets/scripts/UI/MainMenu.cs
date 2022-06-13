using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public float transitionTime;

    public Animator animator;
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void playButton()
    {
        selectLevelMenu.SetActive(true);
    }

    public void playSaharaHunting()
    {
        SceneManager.LoadScene("Sahara hunting");
    }
    public void backToMainMenu()
    {
        selectLevelMenu.SetActive(false);
    }
    IEnumerator Transition(float time)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(time);
        selectLevelMenu.SetActive(true);
    }
}
