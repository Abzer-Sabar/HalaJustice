using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones : MonoBehaviour
{
    public float maxHealth, startTimeBtwShots;
    public Sayah sayah;
    public enemyHealthBar health;
    public GameObject plasmaBall;
    [HideInInspector]
    public bool canShoot = false;

    private float timeBtwShots, currentHealth;


    private void Start()
    {
        timeBtwShots = startTimeBtwShots;
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (canShoot)
        {
            shoot();
        }
    }

   
        private void shoot()
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(plasmaBall, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

    public void Damage(float[] attackDetails)
    {
        Debug.Log("You have damaged me!");
        float damageTaken = attackDetails[0];
        currentHealth -= damageTaken;
        health.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)
        {
            sayah.dronesDestroyed += 1;
            Destroy(gameObject);
            return;
        }
        
    }
}
