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
    public GameObject datePrefab;
    public int datePrice = 10;
    [SerializeField]
    private TextMeshProUGUI goldText;

    [SerializeField]
    private TextMeshProUGUI stopWatchText;

    private float currentTime;

    private bool stopWatchActive = false, TimerStart = false;
    private int goldAmount;
    private int[] possibleGoldAmounts = {10, 11, 15};

    private void Start()
    {
        goldAmount = 0;
        currentTime = 0;
    }

    private void Update()
    {
        if(stopWatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        if(TimerStart == true)
        {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        stopWatchText.text = time.ToString(@"mm\:ss");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dagger"))
        {
            openDialogue();
            setArtifactText(artifactMessages[0]);
            Destroy(collision.gameObject);
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
    public void setGold()
    {
        int length = possibleGoldAmounts.Length;
        int index = UnityEngine.Random.Range(0, length);

        goldAmount += possibleGoldAmounts[index];

        goldText.text = "" + goldAmount;
    }

    private void openDialogue()
    {
        LeanTween.scale(artifactDialogueBox, new Vector3(1f, 1f, 1f), 0.3f).setEase(LeanTweenType.easeInElastic);
    }

    private void setArtifactText(string text)
    {
        artifactTextBox.text = "";
        artifactTextBox.text = text;
    }

    public void closeDialogue()
    {
        LeanTween.scale(artifactDialogueBox, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutElastic);
    }

    public void buyDates()
    {
        if(goldAmount >= datePrice)
        {
            Instantiate(datePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not enough Gold");
        }
    }
}
