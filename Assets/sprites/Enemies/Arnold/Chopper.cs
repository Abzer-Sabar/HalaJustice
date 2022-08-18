using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : MonoBehaviour
{
    public Transform rightLimit, leftLimit;
    public GameObject deathEffect;
    public enemyHealthBar health;
    public float moveSpeed, maxHealth;
    private int _currentWaypointIndex = 0;
    private float currentHealth;
    private bool movingRight;
    private Transform target;

    private void Start()
    {
        currentHealth = maxHealth;
        health.setHealth(currentHealth, maxHealth);
      
    }

    private void Update()
    {
        Vector3 rotate = transform.eulerAngles;
        transform.position = Vector2.MoveTowards(transform.position, rightLimit.position, moveSpeed * Time.deltaTime);
        if (transform.position == rightLimit.position)
        {
            Debug.Log("Position Reached");
            transform.position = Vector2.MoveTowards(transform.position, leftLimit.position, moveSpeed * Time.deltaTime);
            rotate.y = 0f;
        }
        if (transform.position == leftLimit.position)
        {

            transform.position = Vector2.MoveTowards(transform.position, rightLimit.position, moveSpeed * Time.deltaTime);
            rotate.y = 180f;
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
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

    }

    public void flip()
    {
        Vector3 rotate = transform.eulerAngles;

        if (transform.position.x < target.position.x)
        {
            rotate.y = 0f;

        }
        else
        {
            rotate.y = 180f;
        }
        transform.eulerAngles = rotate;
    }

}
