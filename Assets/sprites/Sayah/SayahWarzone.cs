using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayahWarzone : MonoBehaviour
{
    public Sayah sayah;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sayah.playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sayah.playerInRange = true;
        }
    }
}
