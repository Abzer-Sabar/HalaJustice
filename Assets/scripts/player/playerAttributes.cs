using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class playerAttributes : MonoBehaviour
{
    public string[] artifactMessages;
    public TextMeshProUGUI artifactTextBox;
    public GameObject artifactDialogueBox;
   

    private GameManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        artifactDialogueBox.SetActive(false);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dagger"))
        {
            manager.artifactsCollected += 1;
            openDialogue();
            setArtifactText(artifactMessages[0]);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Bow"))
        {
            manager.artifactsCollected += 1;
            openDialogue();
            setArtifactText(artifactMessages[1]);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Arrow"))
        {
            manager.artifactsCollected += 1;
            openDialogue();
            setArtifactText(artifactMessages[2]);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Flag"))
        {
            manager.artifactsCollected += 1;
            openDialogue();
            setArtifactText(artifactMessages[3]);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Pearl"))
        {
            manager.artifactsCollected += 1;
            openDialogue();
            setArtifactText(artifactMessages[4]);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Knife"))
        {
            manager.artifactsCollected += 1;
            openDialogue();
            setArtifactText(artifactMessages[5]);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Fragment1"))
        {
            manager.openFinalDialogue();
            Destroy(collision.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("portal1"))
        {
            manager.openLevelFinishUI();
            var prefab = this.gameObject;
            Destroy(prefab);
        }

        if (collision.gameObject.CompareTag("Dash"))
        {
            manager.openDashDialogue();

        }

        if (collision.gameObject.CompareTag("Expo"))
        {
            FindObjectOfType<Manager2>().updateArtifacts();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("coin"))
        {
            FindObjectOfType<Manager2>().updateArtifacts();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Trophy"))
        {
            FindObjectOfType<Manager2>().updateArtifacts();
            Destroy(collision.gameObject);
        }
    }

    private void openDialogue()
    {
        artifactDialogueBox.SetActive(true);
        LeanTween.scale(artifactDialogueBox, new Vector3(0.5f, 0.5f, 0.5f), 0.3f).setEase(LeanTweenType.easeInElastic);
    }

    private void setArtifactText(string text)
    {
        artifactTextBox.text = "";
        artifactTextBox.text = text;
    }

    public void closeDialogue()
    {
        Debug.Log("you are clicking me");
        LeanTween.scale(artifactDialogueBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutElastic).setOnComplete(disableDialogue);
    }

    private void disableDialogue()
    {
        artifactDialogueBox.SetActive(false);

    }

    
}
