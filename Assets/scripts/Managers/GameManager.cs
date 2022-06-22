using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Dialogues dialogue;
    public GameObject gameplayUI;
    public GameObject startSceneUI;
    public GameObject dialogueBoxUI;
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
                TriggerDialogue();
                StartGame = true;
            }
        }*/
    }

    private void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    private void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void reloadScene()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void mainMenu()
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
        dialogueBoxUI.SetActive(false);
        tutorialsUI.SetActive(false);
    }

    private void showEverything()
    {
        dialogueBoxUI.SetActive(true);
        gameplayUI.SetActive(true);
        tutorialsUI.SetActive(true);
        startSceneUI.SetActive(false);
    }
}
