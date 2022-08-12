using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infantryWarzone : MonoBehaviour
{
    public infantry enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.playerInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.playerInSight = false;
        }
    }
}
