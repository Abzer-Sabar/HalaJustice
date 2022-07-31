using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveBossScript : MonoBehaviour
{
    #region Public Variables
    public Transform rightLimit, leftLimit;
    public float rayCastLength = 6f;
    public float attackDistance = 1f; //Minimum distance for attack
    public float moveSpeed = 2f, maxHealth = 100f;
    public float timer = 2f; //Timer for cooldown between attacks
    public int playerDamage = 10, goldAmount = 30;
    public GameObject hotZone, triggerArea, blockade1, blockade2;
    public GameManager manager;
    [HideInInspector] public bool inRange;
    [HideInInspector] public Transform target;

    //recent changes
    public float range = 2.5f, attackCooldown = 2f, colliderDistance = 0.19f;
    public BoxCollider2D boxCollider;
    public enemyHealthBar enemyHealthBar;
    public GameObject deathEffect;

    public float DamageCooldown = 0.2f, lastDamageTime, damageAreaWidth = 0.64f, damageAreaHeight = 1.55f, touchdamage = 1, speed;
    // public Transform damageCheck;
    public LayerMask whatIsPlayer;
    private Vector2 damageBotLeft, damageTopRight;
    private float[] attackDetails = new float[2];
    #endregion

    #region Private Variables
    //private RaycastHit2D hit;
    [SerializeField]
    private playerHealth ph;
    [SerializeField]
    private Animator anim;
    private float distance; //Store the distance b/w enemy and player
    private bool attackMode;
    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer, currentHealth;

    #endregion


    private void Start()
    {
        currentHealth = maxHealth;
        enemyHealthBar.setHealth(currentHealth, maxHealth);
        speed = moveSpeed;
    }
    void Awake()
    {
        selectTarget();
        intTimer = timer; //Store the inital value of timer
                          // anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {

        if (!attackMode)
        {
            Move();
        }

        if (!InsideBounds() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("miniBoss_Attack"))
        {
            selectTarget();
        }



        if (inRange)
        {
            speed = 4;
            EnemyLogic();
        }
        else
        {
            speed = moveSpeed;
        }
        // touchDamage();

    }


    /* private void touchDamage()
     {
         if (Time.time >= DamageCooldown + lastDamageTime)
         {
             damageBotLeft.Set(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
             damageTopRight.Set(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));

             Collider2D hit = Physics2D.OverlapArea(damageBotLeft, damageTopRight, whatIsPlayer);

             if (hit != null)
             {

                 lastDamageTime = Time.time;
                 attackDetails[0] = touchdamage;
                 attackDetails[1] = this.transform.position.x;
                 hit.transform.SendMessage("Damage", attackDetails);
             }
         }
     }*/

    public void selectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        flip();
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


    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {

            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("attacking", false);
        }
    }

    void Move()
    {
        anim.SetBool("moving", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("miniBoss_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("moving", false);
        anim.SetBool("attacking", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("attacking", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideBounds()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void damagePlayer()
    {

        if (PlayerInSight())
        {
            Debug.Log("The player is in sight");
            ph.takeDamage(playerDamage);
        }
    }

    public void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        enemyHealthBar.setHealth(currentHealth, maxHealth);
        Debug.Log("You have damaged me!");
        if (currentHealth <= 0.0f)
        {
            die();
        }
    }

    private void die()
    {
        blockade1.SetActive(false);
        blockade2.SetActive(false);
        anim.SetTrigger("die");
        this.enabled = false;
        manager.setGold(goldAmount);
        Instantiate(deathEffect, this.transform.position, deathEffect.transform.rotation);
        Destroy(gameObject);
        Debug.Log("Enemy died");
    }



    private void OnDrawGizmos()
    {
        //Vector2 botRight = new Vector2(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
        // Vector2 botLeft = new Vector2(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
        // Vector2 topRight = new Vector2(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));
        // Vector2 topLeft = new Vector2(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));

        //Gizmos.DrawLine(botLeft, botRight);
        // Gizmos.DrawLine(botRight, topRight);
        //Gizmos.DrawLine(topRight, topLeft);
        //Gizmos.DrawLine(topLeft, botLeft);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, whatIsPlayer);

        if (hit.collider != null)
            ph = hit.transform.GetComponent<playerHealth>();

        return hit.collider != null;
    }
}
