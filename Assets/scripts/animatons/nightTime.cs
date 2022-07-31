using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class nightTime : MonoBehaviour
{

    public float nightLightIntensity;

    public GameObject nightBg;
    public GameObject caveBg;
    public GameObject desertBg;
    public GameObject caveParticles;
    public GameObject restriction1;
    public Light2D globalLight;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            changeWeatherToNight();
        }
    }
    private void changeWeatherToNight()
    {
        nightBg.SetActive(true);
        restriction1.SetActive(true);
        caveBg.SetActive(false);
        desertBg.SetActive(false);
        caveParticles.SetActive(false);
        globalLight.intensity = nightLightIntensity;
    }
}
