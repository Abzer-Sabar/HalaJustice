using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayTime : MonoBehaviour
{
    public GameObject dayTimeBg, nightTimeBg;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dayTimeBg.SetActive(true);
            nightTimeBg.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dayTimeBg.SetActive(false);
            nightTimeBg.SetActive(true);
        }
    }
}
