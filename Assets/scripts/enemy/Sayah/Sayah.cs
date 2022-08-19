using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sayah : MonoBehaviour
{
    public Transform memoryPos;
    public GameObject memoryFragment;
    public GameObject[] drones;
    public GameObject specialDrone, shield;
    public enemyHealthBar health;

    public float maxHealth = 100;

    [HideInInspector]
    public int dronesDestroyed;
    [HideInInspector]
    public bool playerInRange = false;

    private float currentHealth, timeBtwShots;
    [SerializeField]
    private bool shieldOn = true;

    
    private enum States
    {
        awake,
        droneAttack,
        specialDroneAttack,
        death
    }

    private States currentState;

    private void Start()
    {
        currentState = States.awake;
        currentHealth = maxHealth;
        health.setHealth(currentHealth, maxHealth);
        specialDrone.SetActive(false);
       
    }

    private void Update()
    {

        switch (currentState)
        {
            case States.awake:
                scanForPlayer();
                disableDrones();
                disableSpecialDrone();
                break;

            case States.droneAttack:
                if (Random.value > 0.5)
                {
                    FindObjectOfType<AudioManager>().play("ImYourMaster");
                }
                else
                {
                    FindObjectOfType<AudioManager>().play("FeelDark");
                }
                enableDrones();
                scanForPlayer();
                checkDrones();
                break;

            case States.specialDroneAttack:
                Debug.Log("Special Drone attack enabled");
                FindObjectOfType<AudioManager>().play("YouBelong");
                enableSpecialDrone();
                checkForPlayer();
                break;

            case States.death:
                break;
        }

  
    }

    

    private void scanForPlayer()
    {
        if (playerInRange)
        {
            currentState = States.droneAttack;
        }
        else
        {
            currentState = States.awake;
        }
    }

    private void checkForPlayer()
    {
        if (playerInRange)
        {
            currentState = States.specialDroneAttack;
        }
        else
        {
            currentState = States.awake;
        }
    }
    private void enableDrones()
    {

        for (int i = 0; i < drones.Length; i++)
        {
            if (drones[i] != null)
            {
                drones[i].gameObject.GetComponent<Drones>().canShoot = true;
            }
        }
    }

    private void disableDrones()
    {
        for (int i = 0; i < drones.Length; i++)
        {
            if (drones[i] != null)
            {
            drones[i].gameObject.GetComponent<Drones>().canShoot = false;    
            }
        }
    }

    private void enableSpecialDrone()
    {
        specialDrone.GetComponent<Drones>().canShoot = true;
    }

    private void disableSpecialDrone()
    {
        specialDrone.GetComponent<Drones>().canShoot = false;
    }

    private void checkDrones()
    {
        if(dronesDestroyed == 4)
        {
            currentState = States.specialDroneAttack;
            shieldOn = false;
            shield.SetActive(false);
            specialDrone.SetActive(true);
        }
    }

    public void BulletDamage(float damage)
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
            Die();
            return;
        }

    }

    public void Damage(float damage)
    {
        Debug.Log("You have damaged me!");
        float damageTaken = damage;
        currentHealth -= damageTaken;
        health.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
            return;
        }

    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().play("SayahDeath");
        Instantiate(memoryFragment, new Vector2(transform.position.x, transform.position.y + 3), Quaternion.identity);
        Destroy(gameObject);
    }
}
