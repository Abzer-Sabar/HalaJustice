using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Manager2 : MonoBehaviour
{
    public GameObject bronzeTrophyPrefab, silverTrophyPrefab, goldTropyPrefab;
    public GameObject levelFinishUI, levelFinishMainMenuButton, levelFinishReplayButton, timeBox, trophy, artifactsBox, deathsBox;
    public GameObject timeTextUI, deathTextUI, artifactsTextUI;
    public GameObject finalDialogueBox, trophyTextObject, dashDialogue;
    public TextMeshProUGUI timeValueText, artifactsValueText, deathsValueText, trophyText;
    [SerializeField]
    private GameObject player, pauseMenu, dialogueBoxUI, portal, optionsMenuUI, fadeTransition;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI stopWatchText, artifactsText;

 
    public int goldAmount;

    [HideInInspector]
    public int goldMultiplier = 1, artifactsCollected = 4, numberOfDeaths = 0, FinishTime = 0, MemoryFragments;
    private float currentTime;
    private static bool gameIsPaused = false;
    private int remainingArtifacts = 0;
    private bool StartGame = false;
    private bool stopWatchActive = false, TimerStart = false;
    private Vector2 startPos, finalPos;

    private void Start()
    {
       
        artifactsCollected = 4;
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
        levelFinishUI.SetActive(false);
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
        FindObjectOfType<AudioManager>().play("popUp");
        LeanTween.scale(dialogueBoxUI, new Vector3(1f, 1f, 1f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(pauseTime);
    }


    private void pauseTime()
    {
        Time.timeScale = 0f;
        player.GetComponent<playerController>().enabled = false;
        player.GetComponent<combat>().enabled = false;
        player.GetComponent<Gun>().enabled = false;
    }
    public void closeIntro()
    {
        Time.timeScale = 1f;
        player.GetComponent<playerController>().enabled = true;
        player.GetComponent<combat>().enabled = true;
        player.GetComponent<Gun>().enabled = true;
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
            fadeIn();
        }
    }

    private void fadeIn()
    {
        fadeTransition.SetActive(true);
        LeanTween.alpha(fadeTransition.GetComponent<RectTransform>(), 1f, 3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(teleportPlayerToSayah);
    }

    private void teleportPlayerToSayah()
    {
        player.GetComponent<playerHealth>().teleportToSayah();
        LeanTween.alpha(fadeTransition.GetComponent<RectTransform>(), 0f, 3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(setFinalArena);
    }

    private void setFinalArena()
    {
        fadeTransition.SetActive(false);
        player.GetComponent<playerHealth>().healPlayer(100);
    }
    public void openOptionsUI()
    {
        optionsMenuUI.SetActive(true);
       // LeanTween.move(optionsMenuUI, finalPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
       // LeanTween.alpha(optionsMenuUI.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(openOptionMenuElements);
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

    //level complete
    public void openLevelFinishUI()
    {
        TimerStart = false;
        levelFinishUI.SetActive(true);
        LeanTween.alpha(levelFinishUI.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(openLevelFinishElements);
        displayStats();
    }

    private void openLevelFinishElements()
    {
        LeanTween.scale(levelFinishMainMenuButton, new Vector3(1f, 1f, 1f), 0.2f).setOnComplete(openTimeBoxUI);
        LeanTween.scale(levelFinishReplayButton, new Vector3(1f, 1f, 1f), 0.2f);
    }


    public void openFinalDialogue()
    {
        FindObjectOfType<AudioManager>().play("popUp");
        finalDialogueBox.SetActive(true);
        LeanTween.scale(finalDialogueBox, new Vector3(0.5f, 0.5f, 0.5f), 0.3f).setEase(LeanTweenType.easeOutElastic);
    }
    public void closeFinalDialogue()
    {
        LeanTween.scale(finalDialogueBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutElastic).setOnComplete(openLevelFinishUI);
        finalDialogueBox.SetActive(false);
    }

    private void openTimeBoxUI()
    {
        timeBox.SetActive(true);
        LeanTween.alpha(timeBox.GetComponent<RectTransform>(), 1f, 1.5f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(displayTimeText);

    }

    private void displayTimeText()
    {
        timeTextUI.SetActive(true);
        openDeathBoxUI();
    }

    private void openDeathBoxUI()
    {
        deathsBox.SetActive(true);
        LeanTween.alpha(deathsBox.GetComponent<RectTransform>(), 1f, 1.5f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(displayDeathText);
    }

    private void displayDeathText()
    {
        deathTextUI.SetActive(true);
        openArtifactsBoxUI();
    }

    private void openArtifactsBoxUI()
    {
        artifactsBox.SetActive(true);
        LeanTween.alpha(artifactsBox.GetComponent<RectTransform>(), 1f, 1.5f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(displayArtifactText);
    }

    private void displayArtifactText()
    {
        artifactsTextUI.SetActive(true);
        calculateScore();
    }

    private void displayTrophy(GameObject trophy)
    {
        trophy.SetActive(true);
        LeanTween.alpha(trophy.GetComponent<RectTransform>(), 1f, 1.5f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(displayTrophyText);
    }
    private void displayTrophyText()
    {

        trophyTextObject.SetActive(true);
    }

    //Final scores calculation
    private void calculateScore()
    {
        FinishTime = (int)currentTime / 60;
        Debug.Log("Time spent" + FinishTime);

        if ( numberOfDeaths <= 3 & FinishTime <= 10)
        {
            Debug.Log("Gold trophy");
            trophyText.text = "" + "Gold";
            displayTrophy(goldTropyPrefab);
        }
        else if ( numberOfDeaths <= 6 & FinishTime <= 15)
        {
            Debug.Log("Silver trophy");
            trophyText.text = "" + "Silver";
            displayTrophy(silverTrophyPrefab);
        }
        else
        {
            Debug.Log("Bronze trophy");
            trophyText.text = "" + "Bronze";
            displayTrophy(bronzeTrophyPrefab);
        }

    }

    private void displayStats()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeValueText.text = time.ToString(@"mm\:ss");
        artifactsValueText.text = artifactsCollected.ToString() + "/4";
        deathsValueText.text = "" + numberOfDeaths.ToString();
    }

    //mouse button sounds
     public void mouseClickSound()
    {
        FindObjectOfType<AudioManager>().play("Click");
    }

}
