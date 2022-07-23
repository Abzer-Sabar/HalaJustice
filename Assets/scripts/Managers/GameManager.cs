using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Dialogues dialogue;
    public playerAttributes playerAtt;
    public GameObject gameplayUI;
    public GameObject startSceneUI;
    public GameObject messages, introDialogueBox;
    public GameObject portal;
    public GameObject tutorialsUI;

    [SerializeField]
    private Transform respawnPlayerPoint;

    [SerializeField]
    private GameObject player, pauseMenu;

    private CinemachineVirtualCamera camera;
    private static bool gameIsPaused = false;
    private bool StartGame = false;

    private void Start()
    {
        camera = GameObject.Find("player Camera").GetComponent<CinemachineVirtualCamera>();
        //HideEverything();
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
        playerAtt.stopStopWatch();
        playerAtt.startTimer();
    }



}
