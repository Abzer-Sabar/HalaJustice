using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public float transitionTime;
    public GameObject flames;
    public Animator animator;

    private void Start()
    {
        flames.SetActive(false);
        FindObjectOfType<AudioManager>().play("Desert Ambient");
        FindObjectOfType<AudioManager>().play("Main");
        StartCoroutine(startFlames());
    }
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
        FindObjectOfType<AudioManager>().stop("Desert Ambient");
        SceneManager.LoadScene("Sahara hunting");
    }
    public void backToMainMenu()
    {
        selectLevelMenu.SetActive(false);
    }
    IEnumerator startFlames()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<AudioManager>().play("FireStart");
        flames.SetActive(true);
        yield return new WaitForSeconds(5f);
        FindObjectOfType<AudioManager>().play("FireBurning");
    }
}
