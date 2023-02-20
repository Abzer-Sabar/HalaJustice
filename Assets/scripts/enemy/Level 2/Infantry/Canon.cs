using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public float fireRate = 2f, plasmaBallSpeed, plasmaBallDamage, shootingDistance = 5, maxHealth;
    public enemyHealthBar health;
    public GameObject plasmaBall, ammo, chunks;
    public Transform firePoint;
    public int goldAmount = 10;

    private float fireCountDown, currentHealth;
    private Transform player;
    private Animator anim;

    private bool canShoot = true, flip, isDead = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shootingDistance = 7f;
        anim = GetComponent<Animator>();
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
           
                anim.SetBool("destroy", true);
                FindObjectOfType<AudioManager>().play("Grenade Explode");
                Instantiate(chunks, transform.position, Quaternion.identity);
                Instantiate(ammo, transform.position, Quaternion.identity);
                FindObjectOfType<Manager2>().setGold(goldAmount);
              
            
        }

    }

   public void die()
    {
        Destroy(gameObject);
    }
}
