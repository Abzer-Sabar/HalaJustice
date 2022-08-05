using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public Dialogues dialogue;
    public playerAttributes playerAtt;
    public GameObject gameplayUI;
    public GameObject startSceneUI;
    public GameObject messages, introDialogueBox;
    public GameObject portal, portal2;
    public GameObject tutorialsUI;
    public GameObject levelFinishUI, levelFinishMainMenuButton, levelFinishTextObject;
    public GameObject finalDialogueBox;

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
    public int goldMultiplier = 1;

    private CinemachineVirtualCamera camera;
    private float currentTime;
    private static bool gameIsPaused = false;
    private bool StartGame = false;
    private bool stopWatchActive = false, TimerStart = false;

    private void Start()
    {
        camera = GameObject.Find("player Camera").GetComponent<CinemachineVirtualCamera>();
        goldAmount = 19;
        setGold(goldAmount);
        currentTime = 0;
        finalDialogueBox.SetActive(false);
        // HideEverything();
    }
    public void respawn()
    {
        var playerTemp = Instantiate(player, respawnPlayerPoint);
        camera.m_Follow = playerTemp.transform;
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
    }

    void spawnPlayer()
    {
        player.SetActive(true);
        LeanTween.scale(player, new Vector3(1f, 1f, 1f), 0f);
        stopStopWatch();
        startTimer();
    }


    //Level complete
    public void openLevelFinishUI()
    {
        LeanTween.alpha(levelFinishUI.GetComponent<RectTransform>(), 1f, 0.3f).setDelay(0f).setEase(LeanTweenType.easeInBack).setOnComplete(openLevelFinishElements);
    }

    private void openLevelFinishElements()
    {
        LeanTween.scale(levelFinishMainMenuButton, new Vector3(1f, 1f, 1f), 0.5f).setDelay(0.5f);
        levelFinishTextObject.SetActive(true);
    }


    public void openFinalDialogue()
    {
        finalDialogueBox.SetActive(true);
        LeanTween.scale(finalDialogueBox, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeOutElastic);
    }
    public void closeFinalDialogue()
    {
        LeanTween.scale(finalDialogueBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutElastic);
        Instantiate(portal2, portalSpawnPosition.position, Quaternion.identity);
        finalDialogueBox.SetActive(false);
    }



}
