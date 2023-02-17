using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : MonoBehaviour
{
    public int goldAmount = 25;
    public float moveSpeed, fireRate = 1f, maxHealth = 100;
    public Transform firePoint;
    public GameObject grenade,chunkParticles;
    public Transform leftPos, rightPos;
    public enemyHealthBar health;
    Vector3 nextpos;
   
    private Vector3 position1, position2, startPosition;
    private float fireCountDown = 0f, currentHealth;

    private void Start()
    {
        position1 = new Vector2(leftPos.position.x, leftPos.position.y);
        position2 = new Vector2(rightPos.position.x, rightPos.position.y);
        startPosition = position2;
        nextpos = startPosition;
        currentHealth = maxHealth;
        health.setHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
       
        if (transform.position == position1)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            nextpos = position2;
        }
        if (transform.position == position2)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            nextpos = position1;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextpos, moveSpeed * Time.deltaTime);
        if (fireCountDown <= 0)
        {
            
            shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    private void shoot()
    {
        Instantiate(grenade, firePoint.position, Quaternion.identity);
    }

    public void Damage(float[] damage)
    {
        float damageTaken = damage[0];
        currentHealth -= damageTaken;
        health.setHealth(currentHealth, maxHealth);
        if (currentHealth <= 0.0f)
        {
            GetComponent<Animator>().SetTrigger("die");
            Instantiate(chunkParticles, transform.position, Quaternion.identity);
            FindObjectOfType<Manager2>().setGold(goldAmount);
            return;
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }
        private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1, position2);
    }

}
