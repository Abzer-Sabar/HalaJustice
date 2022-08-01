using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warZoneCheck : MonoBehaviour
{
    public Boss boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.playerInRange = true;
            Debug.Log("Player is in warzone");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.playerInRange = false;
        }
    }
}
