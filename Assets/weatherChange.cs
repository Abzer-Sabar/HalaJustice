using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherChange : MonoBehaviour
{
    public GameObject desertBg;
    public GameObject caveBg;
    public Light globalLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            changeWeatherToCave();
        }
    }

    private void changeWeatherToCave()
    {
        desertBg.SetActive(false);
        caveBg.SetActive(true);
    }
}
