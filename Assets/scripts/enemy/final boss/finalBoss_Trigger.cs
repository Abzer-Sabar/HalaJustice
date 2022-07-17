using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBoss_Trigger : MonoBehaviour
{
    public Boss boss;
 
    public GameObject hotZoneEffect;

    private void Start()
    {
        hotZoneEffect.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.playerInSight = true;
            gameObject.SetActive(false);
            hotZoneEffect.SetActive(true);
        }
    }
}
