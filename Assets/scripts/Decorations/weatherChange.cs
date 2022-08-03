using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class weatherChange : MonoBehaviour
{
    public float caveLightIntensity, nightLightIntensity;
    public GameObject desertEnemies, caveEnemies, nightEnemies;

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
            desertEnemies.SetActive(false);
            caveEnemies.SetActive(true);
            nightEnemies.SetActive(false);
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
