using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arnold : MonoBehaviour
{
    public GameObject bullet, grenade, deathEffect, chopper;
    public Transform firePoint, leftPos, rightPos;
    public enemyHealthBar health;
    public int goldAmount;
    public float shootingDistance, fireRate, grenadeThrowForce, grenadeThrowChance, maxHealth;

    private bool canShoot, flip, canDeployChopper = true;
    private float fireCountDown = 0f, currentHealth;
    private Transform player;
    private Animator anim;
    private Vector2 dir, target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        health.setHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        target = new Vector2(player.position.x, player.position.y + 3);
        checkForPlayer();
        lookAtPlayer();
        if (canShoot)
        {
            if(fireCountDown <= 0)
            {
                 if(Random.value > grenadeThrowChance)
                 {
                     anim.SetTrigger("shoot");
                 }
                 else
                 {

                     anim.SetTrigger("throw");
                 }
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    private void checkForPlayer()
    {
        if(Vector2.Distance(transform.position, player.position) <= shootingDistance)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

    private void shoot()
    {
        FindObjectOfType<AudioManager>().play("shoot");
        Instantiate(bullet, firePoint.position, Quaternion.identity);
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

    private void throwGrenade()
    {
        GameObject nade = Instantiate(grenade, firePoint.position, Quaternion.identity);
        dir = target - new Vector2(transform.position.x, transform.position.y);
        Rigidbody2D rb = nade.GetComponent<Rigidbody2D>();
        rb.AddForce(dir.normalized * grenadeThrowForce, ForceMode2D.Impulse);
    }

    private void deployChopper()
    {
        if (canDeployChopper)
        {
           GameObject prefab = Instantiate(chopper, new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
            prefab.GetComponent<Chopper>().leftPos = leftPos;
            prefab.GetComponent<Chopper>().rightPos = rightPos;
        }
        else
        {
            return;
        }
    }

    public void Damage(float[] damage)
    {
        Debug.Log("You have damaged me!");
        float damageTaken = damage[0];
        currentHealth -= damageTaken;
        health.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            FindObjectOfType<Manager2>().setGold(goldAmount);
            Destroy(gameObject);
           
        }if(currentHealth <= 50)
        {
            deployChopper();
            canDeployChopper = false;
        }

    }
}
