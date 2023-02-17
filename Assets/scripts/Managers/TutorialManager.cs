using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class TutorialManager : MonoBehaviour
{
    public GameObject swordPlayer, gunPlayer;
    public CinemachineVirtualCamera playerCamera;
    public GameObject pauseMenu, optionsMenu;
    public GameObject switchMenu;

    private bool gameIsPaused;
    private bool swictchMenuActive = false;
    private void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        switchMenu.SetActive(false);
        AudioManager.instance.play("Rain");
        spawnPlayer();

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (swictchMenuActive)
            {
                switchMenu.SetActive(false);
                swictchMenuActive = false;
                Time.timeScale = 1f;
            }
            else
            {
                switchMenu.SetActive(true);
                swictchMenuActive = true;
                Time.timeScale = 0f;
            }
        }

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
            playerCamera.Follow = swordPlayer.transform;
            playerCamera.LookAt = swordPlayer.transform;
            swordPlayer.SetActive(true);
            gunPlayer.SetActive(false);

        }
        else
        {

            playerCamera.Follow = gunPlayer.transform;
            playerCamera.LookAt = gunPlayer.transform;
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

    public void selectSwordPlayer()
    {
        PlayerPrefs.SetInt("swordPlayer", 1);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void selectGunPlayer()
    {
        PlayerPrefs.SetInt("swordPlayer", 0);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
