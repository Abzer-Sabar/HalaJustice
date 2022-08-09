using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sayah : MonoBehaviour
{
    public GameObject[] drones;

    public float maxHealth = 100;

    [HideInInspector]
    public int dronesDestroyed;
    [HideInInspector]
    public bool playerInRange = false;

    private float currentHealth, timeBtwShots;
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
       
    }

    private void Update()
    {
        
        switch (currentState)
        {
            case States.awake:
                scanForPlayer();
                disableDrones();
                break;

            case States.droneAttack:
                enableDrones();
                scanForPlayer();
                checkDrones();
                break;

            case States.specialDroneAttack:
                Debug.Log("Special Drone attack");
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

    private void checkDrones()
    {
        if(dronesDestroyed == 1)
        {
            currentState = States.specialDroneAttack;

        }
    }
}
