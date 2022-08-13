using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones : MonoBehaviour
{
    public float maxHealth, bounceHeight, floatTime;
    public float fireRate = 1f, shieldActiveTime = 5f, startTimeBtwShields = 5f;
    public Transform firePoint;
    public Sayah sayah;
    public enemyHealthBar health;
    public GameObject plasmaBall, shield;
    public bool shieldOn;

    [HideInInspector]
    public bool canShoot = false;

    private float  currentHealth, fireCountDown = 0f, timeBtwShields;


    private void Start()
    {
        currentHealth = maxHealth;
        timeBtwShields = startTimeBtwShields;
        InvokeRepeating("enableShield", 5f, 0.3f);
    }
    private void Update()
    {
        if (canShoot)
        {
            if(fireCountDown <= 0)
            {
                shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

   
        private void shoot()
        {
                Instantiate(plasmaBall, firePoint.position, Quaternion.identity); 
        }

    public void Damage(float damage)
    {
        if (shieldOn)
        {
            return;
        }
        Debug.Log("You have damaged me!");
        float damageTaken = damage;
        currentHealth -= damageTaken;
        health.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)
        {
            sayah.dronesDestroyed += 1;
            Destroy(gameObject);
            return;
        }
        
    }

    IEnumerator enableShield()
    {
        shieldOn = true;
        shield.SetActive(true);
        yield return new WaitForSeconds(shieldActiveTime);
    }
}
