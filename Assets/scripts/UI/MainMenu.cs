using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectMenu, level1Menu, level2Menu;
    public float transitionTime;
    public GameObject flames, flamesParticles;
    public Animator animator;

    private Vector3 finalPos, startPos;
    private void Start()
    {
        flames.SetActive(false);
        flamesParticles.SetActive(false);
        FindObjectOfType<AudioManager>().play("Desert Ambient");
        FindObjectOfType<AudioManager>().play("Main");
        StartCoroutine(startFlames());

        finalPos = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        startPos = new Vector3(Screen.width * 0.5f, 6f, 0);

        levelSelectMenu.SetActive(false);
       
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

  
    IEnumerator startFlames()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().play("FireStart");
        flames.SetActive(true);
        flamesParticles.SetActive(true);
        yield return new WaitForSeconds(5f);
        FindObjectOfType<AudioManager>().play("FireBurning");
    }

    public void playButton()
    {
        levelSelectMenu.SetActive(true);
        LeanTween.alpha(levelSelectMenu.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack);
        LeanTween.move(levelSelectMenu, finalPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
    }

    public void playSaharaHunting()
    {
        FindObjectOfType<AudioManager>().stop("Desert Ambient");
        //FindObjectOfType<AudioManager>().stop("Main");
        SceneManager.LoadScene("Sahara hunting");
    }
    public void backToMainMenu()
    {
        LeanTween.move(levelSelectMenu, startPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.alpha(levelSelectMenu.GetComponent<RectTransform>(), 0f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(disableLevelSelectMenu);
        
    }

    //Level select buttons
    public void level1Button()
    {
        
        LeanTween.scale(level1Menu, new Vector3(1f, 1f, 1f), 0.2f);
    }

    public void level1CloseUI()
    {
        LeanTween.scale(level1Menu, new Vector3(0f, 0f, 0f), 0.2f);
    }

    public void level2Button()
    {
 
        LeanTween.scale(level2Menu, new Vector3(1f, 1f, 1f), 0.2f);
    }

    public void level2CloseUI()
    {
        LeanTween.scale(level2Menu, new Vector3(0f, 0f, 0f), 0.2f);
    }

    private void disableLevelSelectMenu()
    {
        levelSelectMenu.SetActive(false);
    }
}
