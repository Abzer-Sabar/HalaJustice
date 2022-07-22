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
    public GameObject portal;
    public GameObject playerCharacter;
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

        /*if (!StartGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                showEverything();
                startDialogue();
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
        dialogueBoxUI.SetActive(false);
        tutorialsUI.SetActive(false);
        playerCharacter.SetActive(false);
    }

    private void showEverything()
    {
        gameplayUI.SetActive(true);
        tutorialsUI.SetActive(true);
        startSceneUI.SetActive(false);
        portal.SetActive(true);
        playerCharacter.SetActive(true);
    }

    private void startDialogue()
    {
        dialogueBoxUI.SetActive(true);
        LeanTween.scale(dialogueBoxUI, new Vector3(0f, 0f, 0f), 0.5f);
    }


}
