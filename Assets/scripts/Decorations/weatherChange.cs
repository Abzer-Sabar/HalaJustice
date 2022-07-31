using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class weatherChange : MonoBehaviour
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
            changeWeatherToCave();
        }
    }

   


    private void changeWeatherToCave()
    {
        desertBg.SetActive(false);
        caveBg.SetActive(true);
        caveParticles.SetActive(true);
        restriction1.SetActive(true);
        globalLight.intensity = caveLightIntensity;
    }

   
}
