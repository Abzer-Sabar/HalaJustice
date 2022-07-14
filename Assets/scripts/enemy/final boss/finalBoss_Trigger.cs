using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBoss_Trigger : MonoBehaviour
{
    public idleState idlestate;
    public GameObject hotZoneEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            idlestate.playerInSight = true;
            gameObject.SetActive(false);
            hotZoneEffect.SetActive(true);
        }
    }
}
