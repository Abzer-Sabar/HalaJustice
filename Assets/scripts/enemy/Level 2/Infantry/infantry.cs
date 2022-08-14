using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infantry : MonoBehaviour
{
    [HideInInspector]
    public bool playerInRange, playerInSight;
    public float chaseSpeed, stoppingDistance, retreatDistance, starttimeBtwShots, maxHealth, chasingDistance, fireRate = 1f;
    public enemyHealthBar health;
    public GameObject bullet, canon, shield, deathEffect;
    public Transform firePoint, canonSpawnPos;


    private Patrol patrol;
    private Transform player;
    private Animator anim;
    private float currentHealth, fireCountDown = 0f;
    private bool canShoot, shieldOn, canonActive, deployCanon = true;
    private GameObject Canon;
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
        currentHealth = maxHealth;
        health.setHealth(currentHealth, maxHealth);
        shieldOn = false;
        shield.SetActive(false);
    }
    private void Update()
    {
        switch (currenState)
        {
            case States.patrol:
                patrol.canPatrol = true;
                canShoot = false;
                anim.SetBool("shooting", false);
                scanForPlayer();
                break;

            case States.chase:
                patrol.canPatrol = false;
                anim.SetBool("shooting", true);
                chasePlayer();
                lookAtPlayer();
                scanForPlayer();
                break;
        }

        if (canShoot)
        {
            if (fireCountDown <= 0)
            {
                Debug.Log("Enemy is shooting");
                Instantiate(bullet, firePoint.position, Quaternion.identity);
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }

        if (canonActive)
        {
            if(Canon == null)
            {
                shieldOn = false;
                shield.SetActive(false);
            }
        }
    }

    private void scanForPlayer()
    {
        if(Vector2.Distance(transform.position, player.position) < chasingDistance)
        {
            Debug.Log("player is in Range");
            currenState = States.chase;
        }
        else
        {
            Debug.Log("player is out of Range");
            currenState = States.patrol;
        }
    }

    
        private void lookAtPlayer()
        {
            Vector3 scale = transform.localScale;
            if (player.transform.position.x < transform.position.x)
            {
            // scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
            else
            {
            // scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.localScale = scale;
        }
    

    private void chasePlayer()
    {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                  anim.SetBool("shooting", false);
                  transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
                anim.SetBool("shooting", true);
               
            
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -chaseSpeed * Time.deltaTime);
            }
        
        
    }

    public void shoot()
    {
        canShoot = true;
        Debug.Log("trying to shoot");
    }
    
    

    private void die()
    {
        Debug.Log("enemy dead");
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        if (shieldOn)
        {
            return;
        }
        currentHealth -= damage;
        health.setHealth(currentHealth, maxHealth);
        Debug.Log("You have damaged me!");
        if (currentHealth <= 75f)
        {
            anim.SetTrigger("cast");
            cast();

        }
         if (currentHealth <= 0f)
        {

            die();
        }
        
    }

    private void cast()
    {
        if (deployCanon)
        {
            Canon = Instantiate(canon, canonSpawnPos.position, Quaternion.identity);
            canonActive = true;
            shieldOn = true;
            shield.SetActive(true);
            deployCanon = false;
        }
    }

    
}
