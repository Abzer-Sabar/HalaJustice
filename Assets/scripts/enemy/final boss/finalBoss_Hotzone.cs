using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalBoss_Hotzone : MonoBehaviour
{
    public playerHealth health;
    //public finallBoss boss;

    public float hotZoneDamage;

    


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            InvokeRepeating("tickDamage", 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CancelInvoke();
        }
    }

    private void tickDamage()
    {
        health.takeDamage(hotZoneDamage);
        
    }
}
