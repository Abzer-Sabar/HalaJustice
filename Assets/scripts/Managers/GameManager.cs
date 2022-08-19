using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject bronzeTrophyPrefab, silverTrophyPrefab, goldTropyPrefab;
    public Dialogues dialogue;
    public playerAttributes playerAtt;
    public GameObject gameplayUI;
    public GameObject startSceneUI;
    public GameObject messages, introDialogueBox, optionsMenuUI;
    public GameObject portal, portal2;
    public GameObject tutorialsUI;
    public GameObject levelFinishUI, levelFinishMainMenuButton, levelFinishReplayButton, timeBox, trophy, artifactsBox, deathsBox;
    public GameObject timeTextUI, deathTextUI, artifactsTextUI;
    public GameObject finalDialogueBox, trophyTextObject, dashDialogue;

    public TextMeshProUGUI timeValueText, artifactsValueText, deathsValueText, trophyText;
    [SerializeField]
    private Transform respawnPlayerPoint, portalSpawnPosition;

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

    private CinemachineVirtualCamera camera;
    private float currentTime;
    private static bool gameIsPaused = false;
    private bool StartGame = false;
    private bool stopWatchActive = false, TimerStart = false;
    private Vector2 startPos, finalPos;
    
    private void Start()
    {
        camera = GameObject.Find("player Camera").GetComponent<CinemachineVirtualCamera>();
        finalPos = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        startPos = new Vector3(Screen.width * 0.5f, 6f, 0);
        goldMultiplier = 1;
        goldAmount = 100;
        artifactsCollected = 0;
        numberOfDeaths = 0;
        setGold(goldAmount);
        currentTime = 0;
        finalDialogueBox.SetActive(false);
        levelFinishUI.SetActive(false);
        timeTextUI.SetActive(false);
        deathTextUI.SetActive(false);
        artifactsTextUI.SetActive(false);
        timeBox.SetActive(false);
        deathsBox.SetActive(false);
        artifactsBox.SetActive(false);
        goldTropyPrefab.SetActive(false);
        silverTrophyPrefab.SetActive(false);
        bronzeTrophyPrefab.SetActive(false);
        trophyTextObject.SetActive(false);
        dashDialogue.SetActive(false);
        HideEverything();

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

         if (!StartGame)
         {
             if (Input.GetKeyDown(KeyCode.Space))
             {
                 showEverything();
                 startIntro();
                 TriggerDialogue();
                 StartGame = true;
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

    public void openOptionsUI()
    {
        optionsMenuUI.SetActive(true);
        //LeanTween.move(optionsMenuUI, finalPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
       // LeanTween.alpha(optionsMenuUI.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(openOptionMenuElements);
    }

   
    public void closeOptionsUI()
    {
        optionsMenuUI.SetActive(false);
       // LeanTween.scale(optionsGraphicsBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
        //LeanTween.scale(optionsResBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
        //LeanTween.scale(optionsVolumeBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeInCirc);
        //LeanTween.alpha(optionsMenuUI.GetComponent<RectTransform>(), 0f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack);
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
        //pauseElements();
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

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
    }

    private void HideEverything()
    {
        startSceneUI.SetActive(true);
        gameplayUI.SetActive(false);
        messages.SetActive(false);
        tutorialsUI.SetActive(false);
        portal.SetActive(false);
        //player.SetActive(false);
    }

    private void showEverything()
    {
        gameplayUI.SetActive(true);
        tutorialsUI.SetActive(true);
        startSceneUI.SetActive(false);
        messages.SetActive(true);
        //portal.SetActive(true);
       
    }

    private void startIntro()
    {
        LeanTween.scale(introDialogueBox, new Vector3(1f, 1f, 1f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void finishIntro()
    {
        LeanTween.scale(introDialogueBox, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(openPortal);
    }

    void openPortal()
    {
        portal.SetActive(true);
        LeanTween.scale(portal, new Vector3(5.47f, 5.47f, 5.47f), 3f).setDelay(0.5f).setOnComplete(spawnPlayer);
        stopStopWatch();
        startTimer();
    }

    void spawnPlayer()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        player.GetComponent<playerController>().enabled = true;
        player.GetComponent<combat>().enabled = true;
    }
   

    public void openDashDialogue()
    {
        FindObjectOfType<AudioManager>().play("popUp");
        dashDialogue.SetActive(true);
        LeanTween.scale(dashDialogue, new Vector3(0.5f, 0.5f, 0.5f), 0.3f).setEase(LeanTweenType.easeOutElastic).setOnComplete(pauseTime);
    }

    private void pauseTime()
    {
        Time.timeScale = 0f;
    }
    public void closeDashDialogue()
    {
        LeanTween.scale(finalDialogueBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutElastic);
        dashDialogue.SetActive(false);
        Time.timeScale = 1f;
    }


    //Level complete
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
        LeanTween.scale(finalDialogueBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutElastic);
        Instantiate(portal2, portalSpawnPosition.position, Quaternion.identity);
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

        if (artifactsCollected == 5 & numberOfDeaths <= 3 & FinishTime <= 5)
        {
            Debug.Log("Gold trophy");
            trophyText.text = "" + "Gold";
            displayTrophy(goldTropyPrefab);
        }
       else if (artifactsCollected == 3 & numberOfDeaths <= 6 & FinishTime <=7)
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
        artifactsValueText.text = artifactsCollected.ToString() + "/5";
        deathsValueText.text = "" + numberOfDeaths.ToString();
    }
}
