using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    #region Public Variables
    public Transform rightLimit, leftLimit;
    public float rayCastLength;
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed, maxHealth;
    public float timer; //Timer for cooldown between attacks
    public int playerDamage;
    public GameObject hotZone, triggerArea;
    public playerAttributes playerAtt;
    [HideInInspector]public bool inRange;
    [HideInInspector]public Transform target;

    //recent changes
    public float range, attackCooldown, colliderDistance;
    public BoxCollider2D boxCollider;
    public enemyHealthBar enemyHealthBar;
    public GameObject deathEffect;

    public float DamageCooldown, lastDamageTime, damageAreaWidth, damageAreaHeight, touchdamage;
    public Transform damageCheck;
    public LayerMask whatIsPlayer;
    private Vector2 damageBotLeft, damageTopRight;
    private float[] attackDetails = new float[2];
    #endregion

    #region Private Variables
    //private RaycastHit2D hit;
    [SerializeField]
    private playerHealth ph;
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
    }
    void Awake()
    {
        selectTarget();
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {

        if (!attackMode)
        {
            Move();
        }

        if(!InsideBounds() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            selectTarget();
        }
        
       

        if (inRange)
        {
            moveSpeed = 4;
            EnemyLogic();
        }
        else
        {
            moveSpeed = 2;
        }
            touchDamage();

    }


    private void touchDamage()
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
    }

    public void selectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
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

        if(transform.position.x < target.position.x)
        {
            rotate.y = 180f;

        }
        else
        {
            rotate.y = 0f;
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

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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
            ph.takeDamage(playerDamage);
        }
    }

    private void Damage(float[] attackDetails)
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
        anim.SetTrigger("die");
        this.enabled = false;
        playerAtt.setGold();
        Instantiate(deathEffect, this.transform.position, deathEffect.transform.rotation);
        Destroy(gameObject);
        Debug.Log("Enemy died");
    }

    IEnumerator killGameObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Vector2 botRight = new Vector2(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
        Vector2 botLeft = new Vector2(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
        Vector2 topRight = new Vector2(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));
        Vector2 topLeft = new Vector2(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
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
