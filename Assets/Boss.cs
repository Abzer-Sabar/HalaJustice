using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector]
    public bool playerInSight, playerInRange;

    public float StartTimeBtwShots;
    public GameObject plasmaBall;
    public Transform firePoint;

    private float timeBtwShots;
    private State states;
    private enum State
    {
        Sleep,
        Idle,
        FireballAttack
    }

    private void Start()
    {
        timeBtwShots = StartTimeBtwShots;
    }

    private void Update()
    {
        switch (states) {
            case State.Sleep:
                ScanPlayer();
                break;

            case State.Idle:
                Debug.Log("Enemey Idle state");
                checkForPlayer();
                break;

            case State.FireballAttack:
                fireballAttack();
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
        }
        else
        {
            states = State.Idle;
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
}
