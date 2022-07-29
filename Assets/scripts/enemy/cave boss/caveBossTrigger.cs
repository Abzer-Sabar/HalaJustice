using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveBossTrigger : MonoBehaviour
{
    public caveBoss boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.hasPlayerEntered = true;
        }
    }
}
