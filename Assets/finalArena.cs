using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class finalArena : MonoBehaviour
{
    public GameObject finalArenaBg;
    public float nightLightIntensity;
    public Light2D globalLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            finalArenaBg.SetActive(true);
            FindObjectOfType<AudioManager>().play("Rain");
            globalLight.intensity = nightLightIntensity;
            Debug.Log("player is in the final region");
        }
    }
}
