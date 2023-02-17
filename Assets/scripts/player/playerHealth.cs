using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public healthBar hb;
    public GameObject deathEffectParticle;
    public Image armorBar;
    public Transform spawnPosition1, spawnPosition2, spawnPosition3, finalArenaPos;
    public float trapDamage = 10f;
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color silverArmorColor, goldArmorColor;

    [SerializeField]
    private float maxHealth;

    private float currentHealth;
    private float damageReduction = 1f;
    private Vector2 respawnPosition;
    private GameManager manager;
    private playerController pc;

    private Manager2 manager2;

    private combat playerCombat;
    private void Start()
    {
        playerCombat = this.GetComponent<combat>();
        currentHealth = 80;
        hb.setMaxHealth(maxHealth);
        hb.setHealth(currentHealth);
        respawnPosition = new Vector2(spawnPosition1.position.x, spawnPosition1.position.y);
        pc = GetComponent<playerController>();
        manager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        manager2 = GameObject.FindGameObjectWithTag("Manager2").GetComponent<Manager2>();
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
        if (collision.gameObject.tag == "finalArena")
        {
            respawnPosition = new Vector2(finalArenaPos.position.x, finalArenaPos.position.y);
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

    private void Damage(float[] attackDetails)
    {
        if (pc.getDashStatus() == false)
        {
            int direction;
            takeDamage(attackDetails[0]);

            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            pc.applyKnockBack(direction);

        }

    }


    private void Die()
    {
        if(manager)
        {
            manager.numberOfDeaths += 1;

        }
        else if(manager2)
        {
            FindObjectOfType<Manager2>().numberOfDeaths += 1;
        }

        FindObjectOfType<AudioManager>().play("Die");
        Instantiate(deathEffectParticle, this.transform.position, deathEffectParticle.transform.rotation);
        respawn();
        
    }

    public void healPlayer(float heal)
    {
        currentHealth += heal;
        hb.setHealth(currentHealth);
       
    }

    private void respawn()
    {
        transform.position = respawnPosition;
        currentHealth = 50;
        hb.setHealth(currentHealth);
    }

    public void teleportToSayah()
    {
        transform.position = finalArenaPos.position;
        currentHealth = 100;
        hb.setHealth(currentHealth);
    }
    //Armor Upgrade Funtions

    public void silverArmor()
    {
        damageReduction = 1.7f;
        playerCombat.increaseAttackDamage = 10;
        var renderer = GetComponent<Renderer>();
        //renderer.material.SetColor("_Color", Color.white * 1000);
        renderer.material.color = silverArmorColor;
        armorBar.color = Color.white;
        Debug.Log("Player has equipped silver armor");
    }

    public void GoldArmor()
    {
        damageReduction = 2f;
        playerCombat.increaseAttackDamage = 15f;
        var renderer = GetComponent<Renderer>();
        //renderer.material.SetColor("_Color", Color.yellow * 1000);
        renderer.material.color = goldArmorColor;
        armorBar.color = Color.yellow;
        Debug.Log("Player has equipped gold armor");
    }

    public void silverWeapon()
    {
        damageReduction = 1.7f;
        GetComponent<Gun>().increaseBulletDamage(5);
        var renderer = GetComponent<Renderer>();
        //renderer.material.SetColor("_Color", Color.white * 1000);
        renderer.material.color = silverArmorColor;
        armorBar.color = Color.white;
        Debug.Log("Player has equipped silver armor");
    }

    public void GoldWeapon()
    {
        damageReduction = 2f;
        GetComponent<Gun>().increaseBulletDamage(10);
        var renderer = GetComponent<Renderer>();
        //renderer.material.SetColor("_Color", Color.yellow * 1000);
        renderer.material.color = goldArmorColor;
        armorBar.color = Color.yellow;
        Debug.Log("Player has equipped gold armor");
    }



}
