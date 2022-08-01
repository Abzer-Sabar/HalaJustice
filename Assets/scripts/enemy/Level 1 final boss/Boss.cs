using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    
    public bool playerInSight, playerInRange;
    public GameObject[] spawnPoints;
    public float StartTimeBtwShots, maxHealth = 100, damageReduction = 0;
    public GameObject plasmaBall, hotzone;
    public enemyHealthBar healthBar;
    public Transform firePoint;

    private float timeBtwShots, currentHealth;
    private Animator anim;
    private State states;
    private enum State
    {
        Sleep,
        Idle,
        FireballAttack,
        Death
    }

    private void Start()
    {
        timeBtwShots = StartTimeBtwShots;
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (states) {
            case State.Sleep:
                ScanPlayer();
                break;

            case State.Idle:
                Debug.Log("Enemey Idle state");
                anim.SetBool("Awake", true);
                checkForPlayer();
                break;

            case State.FireballAttack:
                fireballAttack();
                break;

            case State.Death:
                die();
                break;
        }


    }

    private void ScanPlayer()
    {
        if (playerInSight)
        {
            states = State.Idle;
        }
    }

    private void checkForPlayer()
    {
        if (playerInRange)
        {
            states = State.FireballAttack;
        }
    }

    private void fireballAttack()
    {
        if (playerInRange)
        {
            shoot();
            anim.SetBool("InRange", true);
        }
        else
        {
            states = State.Idle;
            anim.SetBool("InRange", false);
        }
    }

    private void shoot()
    {
        if(timeBtwShots <= 0)
        {
            Instantiate(plasmaBall, firePoint.position, Quaternion.identity);
            timeBtwShots = StartTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void die()
    {
        Debug.Log("Final boss is Dead!");
        hotzone.SetActive(false);
      
    }

    private void teleport()
    {

    }

    public void Damage(float[] attackDetails)
    {
        float damageTaken = attackDetails[0] - damageReduction;
        currentHealth -= damageTaken;
        healthBar.setHealth(currentHealth, maxHealth);
        Debug.Log("You have damaged me!");

        if (currentHealth <= 0.0f)
        {
            states = State.Death;
        }
    }

  
}
