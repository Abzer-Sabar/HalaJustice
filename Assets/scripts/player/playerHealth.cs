using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public healthBar hb;
    public GameObject deathEffectParticle;
    public float trapDamage = 10f;

    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    private void Start()
    {
        currentHealth = 80;
        hb.setMaxHealth(maxHealth);
        hb.setHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "deadzone")
        {
            Die();
        }

        if (collision.gameObject.tag == "Trap")
        {
            takeDamage(trapDamage);
        }
    }

    
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        hb.setHealth(currentHealth);
        Debug.Log("player taking damage");
        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathEffectParticle, this.transform.position, deathEffectParticle.transform.rotation);
        Destroy(gameObject);
        Destroy(gameObject);
        Debug.Log("You are dead");
    }

    public void healPlayer(float heal)
    {
        currentHealth += heal;
        hb.setHealth(currentHealth);
        Debug.Log("player has healed");
    }


}
