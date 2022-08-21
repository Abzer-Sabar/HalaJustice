using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class finalArena : MonoBehaviour
{
    public GameObject dayTimeBg, nightTimeBg, finalArenaBg, rainParticles;
    public float nightLightIntensity;
    public Light2D globalLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dayTimeBg.SetActive(false);
            nightTimeBg.SetActive(false);
            finalArenaBg.SetActive(true);
            FindObjectOfType<AudioManager>().play("Rain");
            rainParticles.SetActive(true);
            globalLight.intensity = nightLightIntensity;
            Debug.Log("player is in the final region");
        }
    }
}
