using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Manager2 : MonoBehaviour
{
   
    [SerializeField]
    private GameObject player, pauseMenu, dialogueBoxUI, portal, optionsMenuUI;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI stopWatchText, artifactsText;

    [HideInInspector]
    public int goldAmount;

    [HideInInspector]
    public int goldMultiplier = 1, artifactsCollected = 0, numberOfDeaths = 0, FinishTime = 0, MemoryFragments;
    private float currentTime;
    private static bool gameIsPaused = false;
    private int remainingArtifacts = 0;
    private bool StartGame = false;
    private bool stopWatchActive = false, TimerStart = false;
    private Vector2 startPos, finalPos;

    private void Start()
    {
        goldAmount = 200;
        artifactsCollected = 0;
        numberOfDeaths = 0;
        setGold(goldAmount);
        artifactsText.text = "" + remainingArtifacts + "/4";
        currentTime = 0;
        finalPos = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        startPos = new Vector3(Screen.width * 0.5f, 6f, 0);
        hideEverything();
        startIntro();
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

    private void hideEverything()
    {
        optionsMenuUI.SetActive(false);
    }

    private void startIntro()
    {
        portal.SetActive(true);
        LeanTween.scale(portal, new Vector3(5.47f, 5.47f, 5.47f), 3f).setDelay(0.5f).setOnComplete(spawnPlayer);

    }

    private void spawnPlayer()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        player.GetComponent<playerController>().enabled = true;
        player.GetComponent<combat>().enabled = true;
        player.GetComponent<Gun>().enabled = true;
        stopStopWatch();
        startTimer();
        LeanTween.scale(dialogueBoxUI, new Vector3(1f, 1f, 1f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void closeIntro()
    {
        LeanTween.scale(dialogueBoxUI, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
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

    public void stopTimer()
    {
        TimerStart = false;
    }

    public void updateArtifacts()
    {
        remainingArtifacts++;
        artifactsText.text = "" + remainingArtifacts + "/4";

        if(remainingArtifacts == 4)
        {
            player.GetComponent<playerHealth>().teleportToSayah();
        }
    }

    public void openOptionsUI()
    {
        optionsMenuUI.SetActive(true);
       // LeanTween.move(optionsMenuUI, finalPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
       // LeanTween.alpha(optionsMenuUI.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(openOptionMenuElements);
    }

    private void openOptionMenuElements()
    {
       // LeanTween.scale(optionsGraphicsBox, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeInCirc);
       // LeanTween.scale(optionsResBox, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeInCirc);
       // LeanTween.scale(optionsVolumeBox, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeInCirc);
    }
    public void closeOptionsUI()
    {
        optionsMenuUI.SetActive(false);
       // LeanTween.scale(optionsGraphicsBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
       // LeanTween.scale(optionsResBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
      //  LeanTween.scale(optionsVolumeBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
      //  LeanTween.alpha(optionsMenuUI.GetComponent<RectTransform>(), 0f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack);
        //LeanTween.move(optionsMenuUI, startPos, 0.3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(disableOptions);

    }

    private void disableOptions()
    {
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
        ///pauseElements();
        gameIsPaused = true;
    }

  
    public void resumeGame() //button function
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
       // resumeElements();
        closeOptionsUI();
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
