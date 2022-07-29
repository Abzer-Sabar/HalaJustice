using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocks : MonoBehaviour
{
    public LayerMask layer; 

    [SerializeField]
    private GameObject RockParticles;
    private playerHealth health;
    private float damage = 10f;
    private float lifeTime = 5f;
    private float timer;
    
    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.takeDamage(damage);
            Instantiate(RockParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Rocks"))
        {
            return;
        }
        else
        {
            Instantiate(RockParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
