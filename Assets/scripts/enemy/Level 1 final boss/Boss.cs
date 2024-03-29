using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector]
    public bool playerInSight, playerInRange;
    public Transform[] spawnPoints;
    public float StartTimeBtwShots, maxHealth = 100, damageReduction = 0;
    public GameObject plasmaBall, hotzone, deathEffect, memoryFragment, fragmentSpawnPosition;
    public enemyHealthBar healthBar;
    public Transform firePoint;

    private float timeBtwShots, currentHealth;
    private GameObject player;
    private Animator anim;
    private State states;
    private bool flip;
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
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (states) {
            case State.Sleep:
                ScanPlayer();
                break;

            case State.Idle:
                anim.SetBool("Awake", true);
                lookAtPlayer();
                checkForPlayer();
                break;

            case State.FireballAttack:
                lookAtPlayer();
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
        Instantiate(deathEffect, this.transform.position, deathEffect.transform.rotation);
        Instantiate(memoryFragment, fragmentSpawnPosition.transform.position, Quaternion.identity);
        Destroy(gameObject);
        hotzone.SetActive(false);
      
    }

    private void teleport()
    {
        Transform position = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector2 nextPosition = new Vector2(position.position.x, position.position.y);
        transform.position = nextPosition;
    }

    private void lookAtPlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        transform.localScale = scale;
    }

    public void Damage(float[] attackDetails)
    {
        Debug.Log("You have damaged me!");
        float damageTaken = attackDetails[0];
        currentHealth -= damageTaken;
        healthBar.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)
        {
            states = State.Death;
            return;
        }
        teleport();
    }

  
}
