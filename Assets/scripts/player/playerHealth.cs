using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public healthBar hb;
    public GameObject deathEffectParticle;
    public Transform spawnPosition1, spawnPosition2, spawnPosition3;
    public float trapDamage = 10f;

    [SerializeField]
    private float maxHealth;

    private float currentHealth;
    private float damageReduction = 0f;
    private Vector2 respawnPosition;
    private GameManager manager;

    private combat playerCombat;
    private void Start()
    {
        playerCombat = this.GetComponent<combat>();
        currentHealth = 80;
        hb.setMaxHealth(maxHealth);
        hb.setHealth(currentHealth);
        respawnPosition = new Vector2(spawnPosition1.position.x, spawnPosition1.position.y);
        manager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "deadzone")
        {
            Die();
        }
     
        if (collision.gameObject.tag == "checkpoint2")
        {
            respawnPosition = new Vector2(spawnPosition2.position.x, spawnPosition2.position.y);
        }
        if (collision.gameObject.tag == "checkpoint3")
        {
            respawnPosition = new Vector2(spawnPosition3.position.x, spawnPosition3.position.y);
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
        float damageTaken = damage / damageReduction;
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
        manager.numberOfDeaths += 1;
        FindObjectOfType<AudioManager>().play("Die");
        Instantiate(deathEffectParticle, this.transform.position, deathEffectParticle.transform.rotation);
        respawn();
        Debug.Log("You are dead");
    }

    public void healPlayer(float heal)
    {
        currentHealth += heal;
        hb.setHealth(currentHealth);
        Debug.Log("player has healed");
    }

    private void respawn()
    {
        transform.position = respawnPosition;
        currentHealth = 20;
        hb.setHealth(currentHealth);
    }

    //Armor Upgrade Funtions

    public void silverArmor()
    {
        damageReduction = 1.7f;
        playerCombat.increaseAttackDamage = 10;
        Debug.Log("Player has equipped silver armor");
    }

    public void GoldArmor()
    {
        damageReduction = 2f;
        playerCombat.increaseAttackDamage = 15f;
        Debug.Log("Player has equipped gold armor");
    }



}
