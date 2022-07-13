using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBoss_Trigger : MonoBehaviour
{
    public finallBoss boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.playerInSight = true;
            gameObject.SetActive(false);
        }
    }
}
