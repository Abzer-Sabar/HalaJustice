 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int goldAmount = 5;

    [SerializeField]
    private playerAttributes playerAtt;

    [SerializeField]
    private Transform groundCheck, wallCheck, damageCheck;

    [SerializeField]
    private LayerMask WhatIsGround, whatIsPlayer;

    [SerializeField]
    private float groundCheckDistance, wallCheckDistance, moveSpeed, maxHealth, knockBackDuration, lastDamageTime, DamageCooldown, damage, damageAreaWidth, damageAreaHeight;

    [SerializeField]
    private Vector2 knockBackSpeed;

    [SerializeField]
    private GameObject hitParticle, deathEffectParticle;

    private State currenState;
    private bool grounded, wallDetected;

    private Rigidbody2D rb;
    private Animator animator;

    private int facingDirection, damageDirection;
    private float currentHealth, knockbackStartTime;

    private Vector2 movement, damageBotLeft, damageTopRight;

    private GameObject body;
    private float[] attackDetails = new float[2];


    private void Start()
    {
        body = transform.Find("body").gameObject;
        rb = body.GetComponent<Rigidbody2D>();
        animator = body.GetComponent<Animator>();
        currentHealth = maxHealth;

        facingDirection = 1;
    }
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private void Update()
    {
        switch (currenState)
        {
            case State.Walking:
                UpdateWalking();
                break;

            case State.Knockback:
                UpdateKnockback();
                break;

            case State.Dead:
                UpdateDead();
                break;
        }
    }

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        Instantiate(hitParticle, body.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if(attackDetails[1] > body.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        
        if(currentHealth > 0.0f)
        {
            switchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            switchState(State.Dead);
        }
    }

    private void touchDamage()
    {
        if(Time.time >= DamageCooldown + lastDamageTime)
        {
            damageBotLeft.Set(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
            damageTopRight.Set(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(damageBotLeft, damageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastDamageTime = Time.time;
                attackDetails[0] = damage;
                attackDetails[1] = body.transform.position.x;
                hit.transform.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        body.transform.Rotate(0f, 180f, 0f);
    }
    private void switchState(State state)
    {
        switch (currenState)
        {
            case State.Walking:
                ExitWalking();
                break;

            case State.Knockback:
                ExitKnockback();
                break;

            case State.Dead:
                ExitDead();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalking();
                break;

            case State.Knockback:
                EnterKnockback();
                break;

            case State.Dead:
                EnterDead();
                break;
        }

        currenState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 botRight = new Vector2(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
        Vector2 botLeft = new Vector2(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y - (damageAreaHeight / 2));
        Vector2 topRight = new Vector2(damageCheck.position.x + (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));
        Vector2 topLeft = new Vector2(damageCheck.position.x - (damageAreaWidth / 2), damageCheck.position.y + (damageAreaHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }

    // Walking State

    private void EnterWalking()
    {

    }

    private void UpdateWalking()
    {
        grounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, WhatIsGround);

        touchDamage();

        if(!grounded || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(moveSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
        }
    }

    private void ExitWalking()
    {

    }

    // Knockback State

    private void EnterKnockback()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockBackSpeed.x * damageDirection, knockBackSpeed.y);
        rb.velocity = movement;
        animator.SetBool("Knockback", true);
    }

    private void UpdateKnockback()
    {
        if(Time.time >= knockbackStartTime + knockBackDuration)
        {
            switchState(State.Walking);
        }
    }

    private void ExitKnockback()
    {
        animator.SetBool("Knockback", false);
    }

    // Dead State

    private void EnterDead()
    {
        playerAtt.setGold(goldAmount);
        Instantiate(deathEffectParticle, body.transform.position, deathEffectParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDead()
    {

    }

    private void ExitDead()
    {

    }
}
