using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;


    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
    }

  public void StartDialogue(Dialogues dialogue)
    {
  

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

     public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); 
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; 
        }
    }

   
}
