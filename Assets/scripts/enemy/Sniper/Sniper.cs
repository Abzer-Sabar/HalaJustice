using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public bool playerInRange;

    public enemyHealthBar health;

    public float maxHealth;


    private float currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void die()
    {

    }

    public void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        health.setHealth(currentHealth, maxHealth);
        Debug.Log("You have damaged me!");
        if (currentHealth <= 0.0f)
        {
            die();
        }
    }
}
