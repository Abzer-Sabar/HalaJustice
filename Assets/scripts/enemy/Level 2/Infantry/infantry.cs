using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infantry : MonoBehaviour
{
    [HideInInspector]
    public bool playerInRange, playerInSight;
    public float chaseSpeed, stoppingDistance, retreatDistance, starttimeBtwShots, maxHealth;
    public enemyHealthBar health;


    private Patrol patrol;
    private Transform player;
    private Animator anim;
    private float timeBtwShots, currentHealth;

    private enum States
    {
        patrol,
        chase,
    }

    private States currenState;


    private void Start()
    {
        currenState = States.patrol;
        patrol = GetComponent<Patrol>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        timeBtwShots = starttimeBtwShots;
        currentHealth = maxHealth;
    }
    private void Update()
    {
        switch (currenState)
        {
            case States.patrol:
                patrol.canPatrol = true;
                scanForPlayer();
                break;

            case States.chase:
                chasePlayer();
                break;
        }
    }

    private void scanForPlayer()
    {
        if (playerInSight)
        {
            currenState = States.chase;
            patrol.canPatrol = false;
        }
        else
        {
            currenState = States.patrol;
            
        }
    }

    private void lookAtPlayer()
    {

    }

    private void chasePlayer()
    {
        if (playerInSight)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
                anim.SetBool("shooting", true);
                shoot();
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -chaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            currenState = States.patrol;
            anim.SetBool("shooting", false);
        }
        
    }

    private void shoot()
    {
        if(timeBtwShots <= 0)
        {
            timeBtwShots = starttimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
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
