using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class collectibles : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI englishMsgText;

    public string englishMsg;
    public GameObject sprite;


    private void Start()
    {
        
        dialogueBox.SetActive(false);
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.play("Pickup");
            enableDialogue();
            sprite.SetActive(false);
        }
    }

    private void enableDialogue()
    {
        englishMsgText.text = "" + englishMsg;
       
        dialogueBox.SetActive(true);
        LeanTween.scale(dialogueBox, new Vector3(0.5f, 0.5f, 0.5f), 0.3f).setEase(LeanTweenType.easeOutElastic);
    }

    public void closeDialogueBox()
    {
        LeanTween.scale(dialogueBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutElastic).setOnComplete(disableDialogueBox);
    }

    private void disableDialogueBox()
    {
        dialogueBox.SetActive(false);
        Destroy(gameObject);
    }



}
