using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class nightTime : MonoBehaviour
{

    public float caveLightIntensity, nightLightIntensity;

    public GameObject desertBg;
    public GameObject caveBg;
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
        desertBg.SetActive(true);
        caveBg.SetActive(false);
        caveParticles.SetActive(false);
        globalLight.intensity = caveLightIntensity;
    }
}
