using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class playerAttributes : MonoBehaviour
{
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
}
