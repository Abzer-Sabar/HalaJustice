using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Manager2 : MonoBehaviour
{
    [SerializeField]
    private GameObject player, pauseMenu;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI stopWatchText;

    [HideInInspector]
    public int goldAmount;

    [HideInInspector]
    public int goldMultiplier = 1, artifactsCollected = 0, numberOfDeaths = 0, FinishTime = 0, MemoryFragments;
    private float currentTime;
    private static bool gameIsPaused = false;
    private bool StartGame = false;
    private bool stopWatchActive = false, TimerStart = false;

    private void Start()
    {
        goldAmount = 200;
        artifactsCollected = 0;
        numberOfDeaths = 0;
        setGold(goldAmount);
        currentTime = 0;
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

       /* if (!StartGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                showEverything();
                startIntro();
                TriggerDialogue();
                StartGame = true;
            }
        }*/

        if (stopWatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        if (TimerStart == true)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            stopWatchText.text = time.ToString(@"mm\:ss");
        }
    }

    public void startStopWatch()
    {
        stopWatchActive = false;
    }

    public void stopStopWatch()
    {
        stopWatchActive = true;
    }

    public void startTimer()
    {
        TimerStart = true;
    }

    //gold function
    public void setGold(int gold)
    {
        int currentGold = gold * goldMultiplier;
        goldAmount += currentGold;
        goldText.text = "" + goldAmount;
    }

    public void deductGold(int gold)
    {
        goldAmount -= gold;
        goldText.text = "" + goldAmount;
    }

    private void pauseGame() //button function
    {
        pauseMenu.SetActive(true);
        FindObjectOfType<AudioManager>().play("Pause");
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void resumeGame() //button function
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
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
