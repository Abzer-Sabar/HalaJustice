using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public healthBar hb;
    public GameObject deathEffectParticle;
    public float trapDamage = 10f;

    [SerializeField]
    private float maxHealth;

    private float currentHealth;
    private float damageReduction = 0f;

    private combat playerCombat;
    private void Start()
    {
        playerCombat = this.GetComponent<combat>();
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            takeDamage(trapDamage);
        }
    }


    public void takeDamage(float damage)
    {
        float damageTaken = damage - damageReduction;
        currentHealth -= damageTaken;
        FindObjectOfType<AudioManager>().play("Hurt");
        hb.setHealth(currentHealth);
        Debug.Log("player taking damage");
        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().play("Die");
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

    //Armor Upgrade Funtions

    public void silverArmor()
    {
        damageReduction = 5f;
        playerCombat.increaseAttackDamage = 10;
        Debug.Log("Player has equipped silver armor");
    }

    public void GoldArmor()
    {
        damageReduction = 10f;
        playerCombat.increaseAttackDamage = 15f;
        Debug.Log("Player has equipped gold armor");
    }


}
