using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject swordPlayer, gunPlayer;
    public GameObject pauseMenu, optionsMenu;

    private bool gameIsPaused;

    private void Start()
    {
        pauseMenu.SetActive(false);

        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                resumeGame();

            }
            else
            {
                pauseGame();
            }
        }
    }

    private void spawnPlayer()
    {
        swordPlayer.SetActive(false);
        gunPlayer.SetActive(false);
        if (PlayerPrefs.GetInt("swordPlayer") == 1)
        {
            swordPlayer.SetActive(true);
            gunPlayer.SetActive(false);
        }
        else
        {
            swordPlayer.SetActive(false);
            gunPlayer.SetActive(true);
        }
    }

    public void clickSound()
    {
        AudioManager.instance.play("Click");
    }

    public void resumeGame()
    {
       
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void pauseGame()
    {
        AudioManager.instance.play("Pause");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void optionsButton()
    {
        optionsMenu.SetActive(true);
    }

    public void closeOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void reloadScene() //button function
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void mainMenu() //button function
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

}
