using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public float fireRate = 1f, plasmaBallSpeed, plasmaBallDamage, shootingDistance, maxHealth;
    public enemyHealthBar health;
    public GameObject plasmaBall;
    public Transform firePoint;

    private float fireCountDown, currentHealth;
    private Transform player;
    private bool canShoot = false, flip;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentHealth = maxHealth;
        health.setHealth(currentHealth, maxHealth);
    }
    private void Update()
    {
        checkForPlayer();
        lookAtPlayer();
        if (canShoot)
        {
            if (fireCountDown <= 0)
            {
                shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    void checkForPlayer()
    {
        if(Vector2.Distance(transform.position, player.position) < shootingDistance)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

    void shoot()
    {
       GameObject projectile = Instantiate(plasmaBall, firePoint.position, Quaternion.identity);
        projectile.GetComponent<plasmaBall>().moveSpeed = plasmaBallSpeed;
        projectile.GetComponent<plasmaBall>().playerDamage = plasmaBallDamage;
    }

    private void lookAtPlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        transform.localScale = scale;
    }

    public void Damage(float damage)
    {
        Debug.Log("You have damaged me!");
        float damageTaken = damage;
        currentHealth -= damageTaken;
        health.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
            return;
        }

    }
}
